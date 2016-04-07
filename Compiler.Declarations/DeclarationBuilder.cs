using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Declarations
{
	public class DeclarationBuilder
	{
		private readonly PackageSyntax package;

		public DeclarationBuilder(PackageSyntax package)
		{
			this.package = package;
		}

		public IEnumerable<Declaration> Build()
		{
			var globalDeclarations = package.CompilationUnits.SelectMany(cu => cu.Declarations);
			return Build(0, globalDeclarations);
		}

		private IEnumerable<Declaration> Build(int depth, IEnumerable<DeclarationSyntax> syntax)
		{
			return syntax.GroupBy(s => NameInScope(depth, s)).Select(@group =>
			{
				var declarations = BuildClassDeclarations(@group.Key, @group.OfType<ClassSyntax>().ToList())
					.Concat(BuildFunctionDeclarations(@group.Key, @group.OfType<FunctionSyntax>().ToList()))
					.Concat(BuildGlobalVariableDeclarations(@group.OfType<GlobalVariableSyntax>().ToList()))
					.Concat(BuildNamespaceDeclarations(@group.Key, depth, @group.OfType<NamespaceSyntax>().ToList()))
					.ToList();
				return declarations.Count > 1 ? new AmbiguousDeclaration(@group.Key, declarations) : declarations.Single();
			});
		}

		private static string NameInScope(int depth, DeclarationSyntax syntax)
		{
			return syntax.Match().Returning<SyntaxToken>()
				.With<NamespaceSyntax>(ns => ns.Names[depth])
				.With<EntitySyntax>(e => e.Name)
				.Exhaustive()
				.ValueText;
		}
		
		private static IEnumerable<Declaration> BuildClassDeclarations(string name, IReadOnlyList<ClassSyntax> syntax)
		{
			if(!syntax.Any()) return Enumerable.Empty<Declaration>();
			return syntax.GroupBy(@class => @class.IsPartial)
				.SelectMany(@group => @group.Key ? new ClassDeclaration(name, @group).Yield()
					: @group.Select(c => new ClassDeclaration(c)));
		}

		private static IEnumerable<Declaration> BuildFunctionDeclarations(string name, IReadOnlyList<FunctionSyntax> syntax)
		{
			if(!syntax.Any()) return Enumerable.Empty<Declaration>();
			// We don't deal with two overloads having the same parameters here, that happens later
			return new FunctionDeclaration(name, syntax).Yield();
		}

		private static IEnumerable<Declaration> BuildGlobalVariableDeclarations(IEnumerable<GlobalVariableSyntax> syntax)
		{
			return syntax.Select(s => new GlobalVariableDeclaration(s));
		}

		private IEnumerable<Declaration> BuildNamespaceDeclarations(string name, int depth, IReadOnlyList<NamespaceSyntax> syntax)
		{
			if(!syntax.Any()) return Enumerable.Empty<Declaration>();
			var members = Build(depth + 1, syntax.SelectMany(s => ChildSyntax(depth + 1, s)));
			return new NamespaceDeclaration(name, syntax, members).Yield();
		}

		private static IEnumerable<DeclarationSyntax> ChildSyntax(int depth, NamespaceSyntax @namespace)
		{
			if(@namespace.Names.Count == depth)
				return @namespace.Members;

			if(@namespace.Names.Count > depth)
				return @namespace.Yield();

			throw new InvalidOperationException("Namespace syntax was not at the required depth");
		}
	}
}
