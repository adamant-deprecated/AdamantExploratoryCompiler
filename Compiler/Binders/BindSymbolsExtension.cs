using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Binders
{
	public static class BindSymbolsExtension
	{
		public static void BindSymbols(this Package package, SymbolTable symbolTable)
		{
			var packageBinder = new PackageBinder(symbolTable.Package);
			foreach(var compilationUnit in package.CompilationUnits)
				compilationUnit.BindSymbols(packageBinder.GlobalNamespace);
		}

		public static void BindSymbols(this CompilationUnit compilationUnit, ContainerBinder containingScope)
		{
			//var usingDefinitions = compilationUnit.UsingDirectives.SelectMany(u => u.UsingDefinitions(globalScope));
			//var scope = new ContainerBinder(compilationUnit, usingDefinitions);

			//foreach(var declaration in compilationUnit.Declarations)
			//	declaration.BindSymbols(scope);
		}

		//private static IEnumerable<Definition> UsingDefinitions(this UsingDirective usingDirective, GlobalScope globalScope)
		//{
		//	var definition = globalScope.Lookup(usingDirective.NamespaceOrType, DefinitionKind.NamespaceOrType).Resolve();

		//	return definition.Match().Returning<IEnumerable<Definition>>()
		//		.With<NamespaceDefinition>(@namespace => @namespace.Definitions)
		//		.With<EntityDeclaration>(entity => new[] { entity })
		//		.Null(() =>
		//		{
		//			throw new Exception($"Using statement referes to name that does not exist '{usingDirective.NamespaceOrType}'");
		//		})
		//		.Exhaustive();
		//}

		public static void BindNames(this Declaration declaration, ContainerBinder containingScope)
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

		//public static void BindNames(this Statement statement, NameScope scope)
		//{
		//	statement.Match()
		//		.With<ExpressionStatement>(expressionStatement =>
		//		{
		//			expressionStatement.Expression.BindNames(scope);
		//		})
		//		.Exhaustive();
		//}

		//public static void BindNames(this Member member, NameScope scope)
		//{
		//	member.Match()
		//		.With<Field>(field =>
		//		{
		//			field.Type.BindNames(scope);
		//			field.InitExpression?.BindNames(scope);
		//		})
		//		.Exhaustive();
		//}

		//public static void BindNames(this Expression expression, NameScope scope)
		//{
		//	expression.Match()
		//		.With<CallExpression>(call =>
		//		{
		//			call.Expression.BindNames(scope);
		//			foreach(var argument in call.Arguments)
		//				argument.BindNames(scope);
		//		})
		//		.With<MemberExpression>(member =>
		//		{
		//			member.Expression.BindNames(scope);
		//		})
		//		.With<VariableExpression>(variable =>
		//		{
		//			variable.Bind(scope);
		//		})
		//		.Exhaustive();
		//}

		//public static void BindNames(this Type type, NameScope scope)
		//{
		//	type.Match()
		//		.With<TypeName>(typeName =>
		//		{
		//			typeName.Bind(scope);
		//		})
		//		.With<OwnershipType>(ownershipType =>
		//		{
		//			ownershipType.Type.BindNames(scope);
		//		})
		//		.With<ArraySliceType>(arraySliceType =>
		//		{
		//			arraySliceType.ElementType.BindNames(scope);
		//		})
		//		.With<StringType>(stringType =>
		//		{
		//			stringType.Bind(scope);
		//		})
		//		.Ignore<NumericType>()
		//		.Exhaustive();
		//}
	}
}
