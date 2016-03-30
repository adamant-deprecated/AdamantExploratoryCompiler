using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Symbols.NamespaceMembers;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class PackageSymbolBuilder
	{
		private readonly PackageSyntax package;

		public PackageSymbolBuilder(PackageSyntax package)
		{
			this.package = package;
		}

		public PackageSymbol Build(DiagnosticsBuilder diagnostics)
		{
			var namespaceMembers = package.CompilationUnits.SelectMany(cu => BuildNamespaceMembers(cu, diagnostics));
			var globalDeclarations = BuildNamespaceSymbols(namespaceMembers, diagnostics);
			// TODO check for duplicates
			return new PackageSymbol(package, globalDeclarations);
		}

		private IEnumerable<NamespaceMember> BuildNamespaceMembers(CompilationUnitSyntax compilationUnit, DiagnosticsBuilder diagnostics)
		{
			return compilationUnit.Declarations.Select(d => BuildNamespaceMember(d, diagnostics));
		}

		private NamespaceMember BuildNamespaceMember(DeclarationSyntax declaration, DiagnosticsBuilder diagnostics)
		{
			return declaration.Match().Returning<NamespaceMember>()
				.With<NamespaceSyntax>(@namespace =>
				{
					var members = @namespace.Members.Select(m => BuildNamespaceMember(m, diagnostics));

					foreach(var name in @namespace.Names.Reverse())
					{
						var childNamespace = new Namespace(name.ValueText, members);
						members = new[] { childNamespace };
					}
					return members.Single(); // pull out the top level namespace
				})
				.With<ClassSyntax>(@class =>
				{
					// TODO check for and report duplicate members
					var symbol = new ClassSymbol(package, @class.Accessibility, @class.Name.ValueText);
					return new Entity(symbol);
				})
				.With<FunctionSyntax>(function =>
				{
					var symbol = new FunctionSymbol(package, function.Accessibility, function.Name.ValueText);
					return new Entity(symbol);
				})
				.Exhaustive();
		}

		private IEnumerable<DeclarationSymbol> BuildNamespaceSymbols(IEnumerable<NamespaceMember> namespaceMembers, DiagnosticsBuilder diagnostics)
		{
			return namespaceMembers.GroupBy(m => m.GetType()).SelectMany(g =>
			{
				if(g.Key == typeof(Namespace))
					return BuildNamespaceSymbols(g.Cast<Namespace>(), diagnostics);

				if(g.Key == typeof(Entity))
					return g.Cast<Entity>().Select(e => e.Symbol);

				throw new NotSupportedException("Only Entity and Namespace NamespaceMembers are supported");
			});
		}

		private IEnumerable<DeclarationSymbol> BuildNamespaceSymbols(IEnumerable<Namespace> namespaces, DiagnosticsBuilder diagnostics)
		{
			return namespaces.GroupBy(ns => ns.Name).Select(g =>
				// TODO check for and report duplicate symbols
				new NamespaceSymbol(package, g.Key, BuildNamespaceSymbols(g.SelectMany(ns => ns.Member), diagnostics)));
		}
	}
}
