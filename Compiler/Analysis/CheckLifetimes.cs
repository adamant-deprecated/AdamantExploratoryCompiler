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

		public static void CheckLifetimes(this EntityDeclaration declaration)
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
						//TODO parameter.Type.DefaultOwnership(Ownership.BorrowImmutable);
					}

					foreach(var statement in function.Body)
					{
						// TODO
					}
				})
				.With<VariableDeclaration>(global =>
				{
					throw new NotImplementedException();
				})
				.Exhaustive();
		}

		public static void CheckLifetimes(this Member member)
		{
			member.Match()
				.With<Constructor>(constructor =>
				{
					foreach(var parameter in constructor.Parameters)
					{
						//TODO parameter.Type.DefaultOwnership(Ownership.BorrowImmutable);
					}

					throw new NotImplementedException();
				})
				.With<Field>(field =>
				{
					throw new NotImplementedException();
				})
				.Exhaustive();
		}

		public static Ownership CheckLifetimes(this Expression expression)
		{
			return expression.Match().Returning<Ownership>()
				.With<NewObjectExpression>(_ => Ownership.OwnedMutable)
				.With<LiteralExpression>(_ => Ownership.BorrowImmutable)
				.Exhaustive();
		}
	}
}
