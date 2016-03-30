using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Semantics.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	public class PackageBinder : ContainerBinder
	{
		public readonly PackageSyntax PackageSyntax;
		public readonly Package Package;

		// TODO move PackageSymbolReferences into PackageSymbols
		public PackageBinder(PackageSyntax packageSyntax, Package package, IEnumerable<IPackageSymbolReference> packageReferences)
			: base(null, new NamespaceReference(packageReferences.Select(d => d.Package).Append(package)), Enumerable.Empty<ImportedSymbol>())
		{
			Requires.NotNull(packageSyntax, nameof(packageSyntax));

			PackageSyntax = packageSyntax;
			Package = package;
		}

		public override LookupResult LookupInGlobalNamespace(NameSyntax name, PackageSyntax fromPackage)
		{
			return Lookup(name, fromPackage);
		}
	}
}
