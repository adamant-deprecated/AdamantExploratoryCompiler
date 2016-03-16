using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Symbols.Namespaces;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders
{
	/// <summary>
	/// A container binder binds namespace declarations and compilation units. 
	/// </summary>
	public class ContainerBinder : Binder
	{
		private readonly MultiDictionary<string, ImportedSymbol> imports = new MultiDictionary<string, ImportedSymbol>();
		private readonly MergedNamespaceSymbol @namespace;

		public ContainerBinder(Binder containingScope, MergedNamespaceSymbol @namespace)
			: base(containingScope)
		{
			this.@namespace = @namespace;
		}

		public ContainerBinder(Binder containingScope, IEnumerable<ImportedSymbol> imports)
			: base(containingScope)
		{
			foreach(var import in imports)
				this.imports.Add(import.AliasName, import);
		}

		public override LookupResult Lookup(Name name)
		{
			return name.Match().Returning<LookupResult>()
				.With<QualifiedName>(qualifiedName =>
				{
					var context = Lookup(qualifiedName.Left);
					return context.Lookup(qualifiedName.Right);
				})
				.With<IdentifierName>(identifierName =>
				{
					var identifier = identifierName.Identifier.ValueText;
					LookupResult result;

					// First look in the current namespace
					if(@namespace != null)
					{
						result = Resolve(@namespace.GetMembers(identifier));
						if(!result.IsEmpty)
							return result;
					}

					// Then look in the imported names
					var importedSymbols = imports[identifier].Select(x => x.Reference).ToList();
					result = Resolve(importedSymbols);
					if(!result.IsEmpty)
						return result;

					// Then look in containing scopes
					return ContainingScope.Lookup(identifierName);
				})
				.Exhaustive();
		}

		private static LookupResult Resolve(IReadOnlyList<SymbolReference> references)
		{
			var visible = references.Where(r => r.IsVisible).ToList();
			if(visible.Count == 1)
				return LookupResult.Good(visible.Single());

			var visibleInPackage = visible.Where(r => r.InSamePackage).ToList();
			if(visibleInPackage.Count == 1)
				return LookupResult.Good(visibleInPackage.Single()); // TODO issue warning that we have chosen the one in the current package

			if(visibleInPackage.Count > 1 || visible.Count > 1)
				return LookupResult.Ambiguous(visible);

			// Nothing visible in package
			if(references.Count > 0)
				return LookupResult.NotAccessible(references);

			return LookupResult.NotDefined();
		}
	}
}
