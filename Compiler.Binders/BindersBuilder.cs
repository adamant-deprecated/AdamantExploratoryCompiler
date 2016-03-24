using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class BindersBuilder
	{
		private readonly Package packageSyntax;
		private readonly PackageSymbol packageSymbol;
		private readonly List<IPackageSymbolReference> packageReferences;

		public BindersBuilder(
			Package packageSyntax,
			PackageSymbol packageSymbol,
			IEnumerable<IPackageSymbolReference> packageReferences)
		{
			this.packageSyntax = packageSyntax;
			this.packageSymbol = packageSymbol;
			this.packageReferences = packageReferences.ToList();
		}

		public IReadOnlyDictionary<SyntaxNode, Binder> Build(DiagnosticsBuilder diagnostics)
		{
			var binders = new Dictionary<SyntaxNode, Binder>();
			// The package binder doesn't go in the binders collection, it just serves as the root binder
			var packageBinder = new PackageBinder(packageSyntax, packageSymbol, packageReferences);
			foreach(var compilationUnit in packageSyntax.CompilationUnits)
				new CompilationUnitBindersBuilder(binders, packageSyntax, compilationUnit, diagnostics).Build(packageBinder);

			return binders;
		}
	}
}
