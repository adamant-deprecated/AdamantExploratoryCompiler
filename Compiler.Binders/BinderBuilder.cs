using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
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

				foreach(var declaration in compilationUnit.Declarations)
					Build(binders, compilationUnit, declaration, scope, diagnostics);
			}

			return binders;
		}

		private IEnumerable<ImportedSymbol> GatherImportedSymbols(
			UsingDirective usingDirective,
			DiagnosticsBuilder diagnostics,
			CompilationUnit compilationUnit,
			Binder scope)
		{
			var lookup = scope.LookupInGlobalNamespace(usingDirective.Name, packageSyntax);

			if(!lookup.IsViable)
				diagnostics.AddBindingError(compilationUnit.SourceFile, usingDirective.Name.Position, $"Could not bind using statement for {usingDirective.Name}");

			var symbol = lookup.Symbols.Single();
			var @namespace = symbol as NamespaceReference;
			if(@namespace != null)
				return @namespace.GetMembers().Select(m => new ImportedSymbol(m, null));

			return new[] { new ImportedSymbol(symbol, null) };
		}

		public void Build(
			Dictionary<SyntaxNode, Binder> binders,
			CompilationUnit compilationUnit,
			Declaration declaration,
			ContainerBinder containingScope,
			DiagnosticsBuilder diagnostics)
		{
			declaration.Match()
				.With<NamespaceDeclaration>(@namespace =>
				{
					var imports = @namespace.UsingDirectives.SelectMany(u => GatherImportedSymbols(u, diagnostics, compilationUnit, containingScope));
					var namesCount = @namespace.Names.Count;
					for(var i = 0; i < namesCount; i++)
					{
						var name = @namespace.Names[i];
						var last = i == namesCount - 1;
						var reference = containingScope.GetMembers(name.ValueText).OfType<NamespaceReference>().Single();
						containingScope = new NamespaceBinder(containingScope, reference, last ? imports : Enumerable.Empty<ImportedSymbol>());
						// The innermost binder is the one that has the imports and should be associated with the syntax node
						if(last)
							binders[@namespace] = containingScope;
					}

					foreach(var member in @namespace.Members)
						Build(binders, compilationUnit, member, containingScope, diagnostics);
				})
				//		.With<ClassDeclaration>(@class =>
				//		{
				//			// TODO class scope with members defined
				//			foreach(var member in @class.NamedMembers)
				//				member.BindNames(scope);
				//		})
				.With<FunctionDeclaration>(function =>
				{
					throw new NotImplementedException();
					// TODO make a function scope
					//foreach(var statement in function.Body)
					//	statement.BindNames(scope);
				})
				//		.With<VariableDeclaration>(global =>
				//		{
				//			global.Type.BindNames(scope);
				//			global.InitExpression?.BindNames(scope);
				//		})
				.Exhaustive();

		}
	}
}
