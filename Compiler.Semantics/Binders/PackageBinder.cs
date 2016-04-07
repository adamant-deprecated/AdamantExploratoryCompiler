using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Semantics.References;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	internal class PackageBinder : ContainerBinder
	{
		public readonly Package Package;

		// TODO move PackageSymbolReferences into PackageSymbols
		public PackageBinder(Package package)
			: base(null, new NamespaceReference(package.Dependencies.Select(d => d.Package.GlobalNamespace).Append(package.GlobalNamespace)), Enumerable.Empty<ImportedSymbol>())
		{
			Requires.NotNull(package, nameof(package));
			
			Package = package;
		}

		public override LookupResult LookupInGlobalNamespace(NameSyntax name, Package fromPackage)
		{
			return Lookup(name, fromPackage);
		}
	}
}
