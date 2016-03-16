using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Binders.LookupResults;
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

		public PackageBinder(PackageSymbol symbol, IEnumerable<CompiledDependency> dependencies)
			: base(null)
		{
			Requires.NotNull(symbol, nameof(symbol));

			Symbol = symbol;
			var globalNamespaces = dependencies.Select(d => d.Package.Symbols.Package.GlobalNamespace).Append(symbol.GlobalNamespace);
			var mergedGlobalNamespace = new MergedNamespaceSymbol(Symbol, null, globalNamespaces.ToList());
			GlobalNamespace = new ContainerBinder(this, mergedGlobalNamespace);
		}

		public override LookupResult Lookup(Name name)
		{
			return LookupResult.Empty;
		}

		public override LookupResult LookupInGlobalNamespace(Name name)
		{
			return GlobalNamespace.Lookup(name);
		}
	}
}
