using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class PackageBinder : ContainerBinder
	{
		public readonly PackageSyntax PackageSyntax;
		public readonly PackageSymbol PackageSymbol;

		// TODO move PackageSymbolReferences into PackageSymbols
		public PackageBinder(PackageSyntax packageSyntax, PackageSymbol packageSymbol, IEnumerable<IPackageSymbolReference> packageReferences)
			: base(null, new NamespaceReference(packageReferences.Select(d => d.PackageSymbol).Append(packageSymbol)), Enumerable.Empty<ImportedSymbol>())
		{
			Requires.NotNull(packageSyntax, nameof(packageSyntax));

			PackageSyntax = packageSyntax;
			PackageSymbol = packageSymbol;
		}

		public override LookupResult LookupInGlobalNamespace(NameSyntax name, PackageSyntax fromPackage)
		{
			return Lookup(name, fromPackage);
		}
	}
}
