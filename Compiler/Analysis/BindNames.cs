using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;
using Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations;
using Type = Adamant.Exploratory.Compiler.Syntax.Type;

namespace Adamant.Exploratory.Compiler.Analysis
{
	public static class BindNamesExtensions
	{
		public static void BindNames(this CompilationUnit compilationUnit, GlobalScope globalScope)
		{
			var usingDefinitions = compilationUnit.UsingStatements.SelectMany(u => u.UsingDefinitions(globalScope));
			var scope = new CompilationUnitScope(globalScope, usingDefinitions);

			foreach(var declaration in compilationUnit.Declarations)
				declaration.BindNames(globalScope, scope);
		}

		private static IEnumerable<Definition> UsingDefinitions(this UsingStatement usingStatement, GlobalScope globalScope)
		{
			throw new NotImplementedException();
		}

		public static void BindNames(this Declaration declaration, GlobalScope globalScope, NameScope scope)
		{
			declaration.Match()
				.With<NamespaceDeclaration>(@namespace =>
				{
					foreach(var name in @namespace.Name.Parts())
					{
						var definition = (NamespaceDefinition)scope.LookupInScope(name);
						var isFullNamespace = definition.FullyQualifiedName == @namespace.FullyQualifiedName;
						var usingDefinitions = isFullNamespace ? @namespace.UsingStatements.SelectMany(u => u.UsingDefinitions(globalScope))
																: Enumerable.Empty<Definition>();
						scope = new NamespaceScope(definition, usingDefinitions);
					}

					foreach(var nestedDeclaration in @namespace.Declarations)
						nestedDeclaration.BindNames(globalScope, scope);
				})
				.With<ClassDeclaration>(@class =>
				{
					// TODO class scope with members defined
					foreach(var member in @class.Members)
						member.BindNames(scope);
				})
				.With<FunctionDeclaration>(function =>
				{
					// TODO make a function scope
					foreach(var statement in function.Body)
						statement.BindNames(scope);
				})
				.With<VariableDeclaration>(global =>
				{
					global.Type.BindNames(scope);
					global.InitExpression?.BindNames(scope);
				})
				.Exhaustive();
		}

		public static void BindNames(this Statement statement, NameScope scope)
		{
			throw new NotImplementedException();
		}

		public static void BindNames(this Member member, NameScope scope)
		{
			throw new NotImplementedException();
		}

		public static void BindNames(this Expression expression, NameScope scope)
		{
			throw new NotImplementedException();
		}

		public static void BindNames(this Type type, NameScope scope)
		{
			throw new NotImplementedException();
		}
	}
}
