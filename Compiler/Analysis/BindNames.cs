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
			FullyQualifiedName currentItemName = null;
			var definitions = globalScope.Definitions;

			foreach(var name in usingStatement.UsingName.Namespace().Parts())
			{
				currentItemName = currentItemName.Append(name);
				var definition = definitions.TryGetValue(name);
				if(definition == null)
					throw new Exception($"Using statement referes to name that does not exist '{currentItemName}'");

				definition.Match()
					.With<NamespaceDefinition>(@namespace => definitions = @namespace.Definitions)
					.With<ClassDeclaration>(@class =>
					{
						/* TODO get static definitions */
						throw new NotImplementedException();
					})
					.With<EntityDeclaration>(entity =>
					{
						throw new Exception($"Using statement referes to name that does not exist '{usingStatement.UsingName}'");
					})
					.Exhaustive();
			}

			return definitions.TryGetValue(usingStatement.UsingName.Name()).Match().Returning<IEnumerable<Definition>>()
				.With<NamespaceDefinition>(@namespace => @namespace.Definitions)
				.With<EntityDeclaration>(entity => new[] { entity })
				.Null(() =>
				{
					throw new Exception($"Using statement referes to name that does not exist '{usingStatement.UsingName}'");
				})
				.Exhaustive();
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
