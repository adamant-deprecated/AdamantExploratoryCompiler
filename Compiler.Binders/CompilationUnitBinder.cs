using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class CompilationUnitBinder : ContainerBinder
	{
		private readonly CompilationUnit compilationUnit;

		public CompilationUnitBinder(PackageBinder containingScope, CompilationUnit compilationUnit, IEnumerable<ImportedSymbol> imports)
			: base(containingScope, null, imports)
		{
			this.compilationUnit = compilationUnit;
		}

		public void BuildSemanticModel(DiagnosticsBuilder diagnostics)
		{
			// TODO not implemented
			foreach(var declaration in compilationUnit.Declarations)
			{
				//	declaration.Match()
				//		.With<NamespaceDeclaration>(@namespace =>
				//		{
				//			foreach(var name in @namespace.Name.Parts())
				//			{
				//				var definition = (NamespaceDefinition)scope.LookupInCurrentScopeOnly(name, DefinitionKind.NamespaceOrType);
				//				var isFullNamespace = definition.FullyQualifiedName == @namespace.FullyQualifiedName;
				//				var usingDefinitions = isFullNamespace ? @namespace.UsingDirectives.SelectMany(u => u.UsingDefinitions(globalScope))
				//														: Enumerable.Empty<Definition>();
				//				scope = new ContainerBinder(scope, definition, usingDefinitions);
				//			}

				//			foreach(var nestedDeclaration in @namespace.Declarations)
				//				nestedDeclaration.BindNames(globalScope, scope);
				//		})
				//		.With<ClassDeclaration>(@class =>
				//		{
				//			// TODO class scope with members defined
				//			foreach(var member in @class.NamedMembers)
				//				member.BindNames(scope);
				//		})
				//		.With<FunctionDeclaration>(function =>
				//		{
				//			// TODO make a function scope
				//			foreach(var statement in function.Body)
				//				statement.BindNames(scope);
				//		})
				//		.With<VariableDeclaration>(global =>
				//		{
				//			global.Type.BindNames(scope);
				//			global.InitExpression?.BindNames(scope);
				//		})
				//		.Exhaustive();
			}
		}
	}
}
