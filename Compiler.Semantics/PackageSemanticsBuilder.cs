using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Semantics.NamespaceMembers;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public class PackageSemanticsBuilder
	{
		private readonly PackageSyntax packageSyntax;

		public PackageSemanticsBuilder(PackageSyntax packageSyntax)
		{
			this.packageSyntax = packageSyntax;
		}

		public Package Build(DiagnosticsBuilder diagnostics)
		{
			var namespaceMembers = packageSyntax.CompilationUnits.SelectMany(cu => BuildNamespaceMembers(cu, diagnostics));
			var globalDeclarations = BuildNamespaceSymbols(namespaceMembers, diagnostics);
			// TODO check for duplicates
			var package = new Package(packageSyntax, globalDeclarations);

			return package;
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
						var childNamespace = new NamespaceMembers.Namespace(name.ValueText, members);
						members = new[] { childNamespace };
					}
					return members.Single(); // pull out the top level namespace
				})
				.With<ClassSyntax>(@class =>
				{
					// TODO check for and report duplicate members
					var symbol = new Class(packageSyntax, @class.Accessibility, @class.Name.ValueText);
					return new NamespaceMembers.Entity(symbol);
				})
				.With<FunctionSyntax>(function =>
				{
					var symbol = new Function(packageSyntax, function.Accessibility, function.Name.ValueText);
					return new NamespaceMembers.Entity(symbol);
				})
				.Exhaustive();
		}

		private IEnumerable<Declaration> BuildNamespaceSymbols(IEnumerable<NamespaceMember> namespaceMembers, DiagnosticsBuilder diagnostics)
		{
			return namespaceMembers.GroupBy(m => m.GetType()).SelectMany(g =>
			{
				if(g.Key == typeof(NamespaceMembers.Namespace))
					return BuildNamespaceSymbols(g.Cast<NamespaceMembers.Namespace>(), diagnostics);

				if(g.Key == typeof(NamespaceMembers.Entity))
					return g.Cast<NamespaceMembers.Entity>().Select(e => e.Symbol);

				throw new NotSupportedException("Only Entity and Namespace NamespaceMembers are supported");
			});
		}

		private IEnumerable<Declaration> BuildNamespaceSymbols(IEnumerable<NamespaceMembers.Namespace> namespaces, DiagnosticsBuilder diagnostics)
		{
			return namespaces.GroupBy(ns => ns.Name).Select(g =>
				// TODO check for and report duplicate symbols
				new Namespace(packageSyntax, g.Key, BuildNamespaceSymbols(g.SelectMany(ns => ns.Member), diagnostics)));
		}
	}
}
