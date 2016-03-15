using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Symbols.Namespaces;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class PackageSymbolsBuilder
	{
		private readonly Package package;
		private DiagnosticsBuilder diagnostics;
		private PackageSymbol packageSymbol;
		private Dictionary<SyntaxNode, Symbol> symbols;

		public PackageSymbolsBuilder(Package package)
		{
			this.package = package;
		}

		public PackageSymbols Build(DiagnosticsBuilder diagnostics)
		{
			this.diagnostics = diagnostics;
			symbols = new Dictionary<SyntaxNode, Symbol>();
			packageSymbol = new PackageSymbol(package);
			foreach(var compilationUnit in package.CompilationUnits)
				Build(compilationUnit);

			var symbolTable = new PackageSymbols(packageSymbol, symbols);
			// To be safe in case someone calls Build() again for some inexplicable reason
			packageSymbol = null;
			symbols = null;

			return symbolTable;
		}

		private void Build(CompilationUnit compilationUnit)
		{
			foreach(var declaration in compilationUnit.Declarations)
				Build(declaration, packageSymbol.PackageGlobalNamespace);
		}

		private void Build(Declaration declaration, PackageNamespaceSymbol containingNamespace)
		{
			declaration.Match()
				.With<NamespaceDeclaration>(@namespace =>
				{
					foreach(var name in @namespace.Names)
					{
						var existingSymbols = containingNamespace.GetMembers(name.ValueText);
						var existingNamespace = existingSymbols.OfType<PackageNamespaceSymbol>().SingleOrDefault();
						if(existingNamespace != null)
						{
							containingNamespace = existingNamespace;
							continue;
						}

						if(existingSymbols.Count > 0)
							foreach(var symbol in existingSymbols)
							{
								// TODO report duplicate error on symbol, need to worry about all locations
								//diagnostics.AddBindingError(symbol.ContainingPackage, TODO, TODO);
							}

						// TODO need to be dealing with locations here
						containingNamespace.Add(new PackageNamespaceSymbol(packageSymbol, containingNamespace, name.ValueText));
					}
					symbols.Add(@namespace, containingNamespace);
					foreach(var member in @namespace.Members)
						Build(member, containingNamespace);
				})
				.With<ClassDeclaration>(@class =>
				{
					// TODO check for and report duplicate symbols
					var symbol = new ClassSymbol(packageSymbol, containingNamespace, @class.Accessibility, @class.Name.ValueText);
					containingNamespace.Add(symbol);
					symbols.Add(@class, symbol);
				})
				.With<FunctionDeclaration>(function =>
				{
					var symbol = new FunctionSymbol(packageSymbol, containingNamespace, function.Accessibility, function.Name.ValueText);
					containingNamespace.Add(symbol);
					symbols.Add(function, symbol);
				})
				.Exhaustive();
		}
	}
}
