using System;
using System.Collections.Generic;
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
		public static void BindNames(this CompilationUnit compilationUnit, GlobalSymbols projectGlobals, IReadOnlyCollection<GlobalSymbols> globals)
		{
			var usingScope = new UsingScope(null); // TODO correctly construct this
			foreach(var declaration in compilationUnit.Declarations)
				declaration.BindNames(usingScope, projectGlobals, globals);
		}

		public static void BindNames(this Declaration declaration, NameScope scope, GlobalSymbols projectGlobals, IReadOnlyCollection<GlobalSymbols> globals)
		{
			declaration.Match()
				.With<CompilationUnit>(_ => { throw new NotSupportedException("CompilationUnit can't be nested inside declarations"); })
				.With<NamespaceDeclaration>(@namespace => { throw new NotImplementedException(); })
				.With<ClassDeclaration>(@class =>
				{
					foreach(var member in @class.Members)
						member.BindNames(scope);
				})
				.With<FunctionDeclaration>(function =>
				{
					// TODO make a function scope
					foreach(var statement in function.Body)
						statement.BindNames(scope);
				})
				.With<GlobalDeclaration>(global =>
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
