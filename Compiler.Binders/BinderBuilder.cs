using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Directives;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class BinderBuilder
	{
		private readonly Package packageSyntax;
		private readonly PackageSymbol packageSymbol;
		private readonly List<IPackageSymbolReference> packageReferences;

		public BinderBuilder(
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
			{
				var imports = compilationUnit.UsingDirectives.SelectMany(u => GatherImportedSymbols(u, diagnostics, compilationUnit, packageBinder));
				var scope = new CompilationUnitBinder(packageBinder, compilationUnit, imports);

				scope.BuildSemanticModel(diagnostics);
			}

			return binders;
		}

		private IEnumerable<ImportedSymbol> GatherImportedSymbols(
			UsingDirective usingDirective,
			DiagnosticsBuilder diagnostics,
			CompilationUnit compilationUnit,
			PackageBinder packageBinder)
		{
			var lookup = packageBinder.LookupInGlobalNamespace(usingDirective.Name, packageSyntax);

			if(!lookup.IsViable)
				diagnostics.AddBindingError(compilationUnit.SourceFile, usingDirective.Name.Position, $"Could not bind using statement for {usingDirective.Name}");

			var symbol = lookup.Symbols.Single();
			var @namespace = symbol as NamespaceReference;
			if(@namespace != null)
				return @namespace.GetMembers().Select(m => new ImportedSymbol(m, null));

			return new[] { new ImportedSymbol(symbol, null) };
		}
	}
}
