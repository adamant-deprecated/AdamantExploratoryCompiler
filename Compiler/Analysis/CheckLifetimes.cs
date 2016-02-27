using System;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;
using Adamant.Exploratory.Compiler.Syntax.Expressions;
using Adamant.Exploratory.Compiler.Syntax.Members;
using Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Analysis
{
	public static class CheckLifetimesExtensions
	{
		public static void CheckLifetimes(this CompilationUnit compilationUnit)
		{
			foreach(var declaration in compilationUnit.Entities)
				declaration.CheckLifetimes();
		}

		public static void CheckLifetimes(this Declaration declaration)
		{
			declaration.Match()
				.With<ClassDeclaration>(@class =>
				{
					foreach(var member in @class.Members)
					{
						member.CheckLifetimes();
					}
				})
				.With<FunctionDeclaration>(function =>
				{
					foreach(var parameter in function.Parameters)
					{
						//TODO parameter.Type.DefaultOwnership(Ownership.ImmutableBorrow);
					}

					foreach(var statement in function.Body)
					{
						// TODO
					}
				})
				.With<GlobalDeclaration>(global =>
				{
					if(global.InitExpression == null)
						global.Type.DefaultOwnership(Ownership.Owned);
					else
					{
						var expressionOwnership = global.InitExpression.CheckLifetimes();
						global.Type.AssignValueWithOwnership(expressionOwnership);
					}
				})
				.With<CompilationUnit>(_ => { throw new NotSupportedException("CompiliationUnit not supported"); })
				.Exhaustive();
		}

		public static void CheckLifetimes(this Member member)
		{
			member.Match()
				.With<Constructor>(constructor => {
					foreach(var parameter in constructor.Parameters)
					{
						//TODO parameter.Type.DefaultOwnership(Ownership.ImmutableBorrow);
					}

					throw new NotImplementedException();
				})
				.With<Field>(field => {
					if(field.InitExpression == null)
						field.Type.DefaultOwnership(Ownership.Owned);
					else
					{
						var expressionOwnership = field.InitExpression.CheckLifetimes();
						field.Type.AssignValueWithOwnership(expressionOwnership);
					}
				})
				.Exhaustive();
		}

		public static Ownership CheckLifetimes(this Expression expression)
		{
			return expression.Match().Returning<Ownership>()
				.With<NewObjectExpression>(_ => Ownership.Owned)
				.With<LiteralExpression>(_ => Ownership.ImmutableBorrow)
				.Exhaustive();
		}
	}
}
