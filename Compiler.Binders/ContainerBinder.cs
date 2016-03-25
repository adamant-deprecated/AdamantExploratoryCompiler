using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders
{
	/// <summary>
	/// A container binder represents one of package, compilation unit or namespace
	/// </summary>
	public abstract class ContainerBinder : Binder
	{
		private readonly NamespaceReference mergedContainer;
		private readonly MultiDictionary<string, ImportedSymbol> imports = new MultiDictionary<string, ImportedSymbol>();

		internal ContainerBinder(Binder containingScope, NamespaceReference mergedContainer, IEnumerable<ImportedSymbol> imports)
			: base(containingScope)
		{
			this.mergedContainer = mergedContainer;
			foreach(var import in imports)
				this.imports.Add(import.AliasName, import);
		}

		public override IEnumerable<SymbolReference> GetMembers(string name)
		{
			return mergedContainer.GetMembers(name);
		}

		protected override LookupResult Lookup(IdentifierNameSyntax name, Package fromPackage)
		{
			var identifier = name.Identifier.ValueText;

			// First look in the current container
			var result = Resolve(mergedContainer.GetMembers(identifier), fromPackage);
			if(!result.IsEmpty)
				return result;

			// Then look in the imported names
			var importedSymbols = imports[identifier].Select(x => x.Reference).ToList();
			result = Resolve(importedSymbols, fromPackage);
			if(!result.IsEmpty)
				return result;

			// Then look in containing scopes
			if(ContainingScope != null)
				return ContainingScope.Lookup(name, fromPackage);

			return LookupResult.Empty;
		}

		private static LookupResult Resolve(IEnumerable<SymbolReference> symbols, Package fromPackage)
		{
			var symbolsList = symbols.ToList();
			var visible = symbolsList.Where(r => r.IsVisibleFrom(fromPackage)).ToList();
			if(visible.Count == 1)
				return LookupResult.Good(visible.Single());

			var visibleInPackage = visible.Where(r => r.IsIn(fromPackage)).ToList();
			if(visibleInPackage.Count == 1)
				return LookupResult.Good(visibleInPackage.Single()); // TODO issue warning that we have chosen the one in the current package

			if(visibleInPackage.Count > 1 || visible.Count > 1)
				return LookupResult.Ambiguous(visible);

			// Nothing visible in package
			if(symbolsList.Count > 0)
				return LookupResult.NotAccessible(symbolsList);

			return LookupResult.NotDefined();
		}
	}
}
