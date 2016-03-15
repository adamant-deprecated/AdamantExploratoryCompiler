using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Compiled;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Symbols.Namespaces;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class PackageBinder : Binder
	{
		public readonly PackageSymbol Symbol;
		public readonly ContainerBinder GlobalNamespace;
		private readonly List<CompiledDependency> dependencies;

		public PackageBinder(PackageSymbol symbol, IEnumerable<CompiledDependency> dependencies)
			: base(null)
		{
			Requires.NotNull(symbol, nameof(symbol));

			Symbol = symbol;
			GlobalNamespace = new ContainerBinder(this, symbol.GlobalNamespace);
			this.dependencies = dependencies.ToList();
		}

		public IReadOnlyList<CompiledDependency> Dependencies => dependencies;

		public override LookupResult Lookup(Name name)
		{
			return GlobalNamespace.Lookup(name);
		}

		public override LookupResult LookupInGlobalNamespace(Name name)
		{
			return name.Match().Returning<LookupResult>()
				.With<QualifiedName>(qualifiedName =>
				{
					var context = LookupInGlobalNamespace(qualifiedName.Left);
					//return context.Lookup(qualifiedName.Right);
					throw new NotImplementedException();
				})
				.With<IdentifierName>(identifierName =>
				{
					var identifier = identifierName.Identifier.ValueText;
					var possibleReferences = dependencies.SelectMany(d => Lookup(d.Package.Symbols.Package.PackageGlobalNamespace, identifier, false))
						.Concat(Lookup(Symbol.PackageGlobalNamespace, identifier, true)).ToList();
					//return Lookup(possibleReferences);
					throw new NotImplementedException();
				})
				.Exhaustive();
		}

		private static IEnumerable<SymbolReference> Lookup(NamespaceSymbol @namespace, string name, bool inSamePackage)
		{
			return @namespace.GetMembers(name).Select(symbol => new SymbolReference(symbol, inSamePackage));
		}

		private static SymbolReference Lookup(IList<SymbolReference> references)
		{
			var visible = references.Where(r => r.IsVisible).ToList();
			if(visible.Count == 1)
				return visible.Single();

			var visibleInPackage = visible.Where(r => r.InSamePackage).ToList();
			if(visibleInPackage.Count == 1)
				return visibleInPackage.Single(); // TODO issue warning that we have chosen the one in the current package

			//			if(visibleInPackage.Count > 1 || visibile.Count > 1)
			//				throw new Exception($"Ambigous symbol {Symbol}");

			//			// Nothing visible in package
			//			if(definitions.Count > 0)
			//				throw new Exception($"Symbol is not accessible {Symbol}");

			//			throw new Exception("Symbol not defined");

			throw new NotImplementedException();
		}
	}
}
