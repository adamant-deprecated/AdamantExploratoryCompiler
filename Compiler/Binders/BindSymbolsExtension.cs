using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Compiled;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Directives;

namespace Adamant.Exploratory.Compiler.Binders
{
	public static class BindSymbolsExtension
	{
		public static void BindSymbols(this Package package, PackageSymbols symbols, IEnumerable<CompiledDependency> compiledDependencies)
		{
			var packageBinder = new PackageBinder(symbols.Package, compiledDependencies);
			foreach(var compilationUnit in package.CompilationUnits)
				compilationUnit.BindSymbols(packageBinder.GlobalNamespace);
		}

		public static void BindSymbols(this CompilationUnit compilationUnit, ContainerBinder containingScope)
		{
			var imports = compilationUnit.UsingDirectives.SelectMany(u => u.Imports(containingScope));
			var scope = new ContainerBinder(containingScope, imports);

			foreach(var declaration in compilationUnit.Declarations)
				declaration.BindSymbols(scope);
		}

		private static IEnumerable<ImportedSymbol> Imports(
			this UsingDirective usingDirective,
			ContainerBinder containingScope)
		{
			var imports = containingScope.Package.Dependencies
				.Select(d => d.Package.Symbols.Package.GlobalNamespace)
				.Aggregate(Enumerable.Empty<ImportedSymbol>(), (i, ns) => i.Concat(usingDirective.Imports(containingScope, ns, false)));

			return imports.Concat(usingDirective.Imports(containingScope, containingScope.Package.Symbol.GlobalNamespace, true));
		}


		private static IEnumerable<ImportedSymbol> Imports(
			this UsingDirective usingDirective,
			ContainerBinder containingScope,
			NamespaceSymbol @namespace,
			bool isSamePackage)
		{
			var symbols = new List<Symbol>() { @namespace };
			//foreach(var VARIABLE in usingDirective.NamespaceOrType)
			//{

			//}
			throw new NotImplementedException();
		}

		public static void BindSymbols(this Declaration declaration, ContainerBinder containingScope)
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

		//public static void BindSymbols(this Statement statement, NameScope scope)
		//{
		//	statement.Match()
		//		.With<ExpressionStatement>(expressionStatement =>
		//		{
		//			expressionStatement.Expression.BindNames(scope);
		//		})
		//		.Exhaustive();
		//}

		//public static void BindSymbols(this Member member, NameScope scope)
		//{
		//	member.Match()
		//		.With<Field>(field =>
		//		{
		//			field.Type.BindNames(scope);
		//			field.InitExpression?.BindNames(scope);
		//		})
		//		.Exhaustive();
		//}

		//public static void BindSymbols(this Expression expression, NameScope scope)
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

		//public static void BindSymbols(this Type type, NameScope scope)
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
