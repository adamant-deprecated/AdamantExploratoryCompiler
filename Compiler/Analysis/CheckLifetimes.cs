using System;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Expressions;
using Adamant.Exploratory.Compiler.Syntax.Members;

namespace Adamant.Exploratory.Compiler.Analysis
{
	public static class CheckLifetimesExtensions
	{
		public static void CheckLifetimes(this CompilationUnitSyntax compilationUnit)
		{
			throw new NotImplementedException();
		}

		public static void CheckLifetimes(this EntitySyntax declaration)
		{
			declaration.Match()
				.With<ClassSyntax>(@class =>
				{
					throw new NotImplementedException();
				})
				.With<FunctionSyntax>(function =>
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
				.With<GlobalVariableSyntax>(global =>
				{
					throw new NotImplementedException();
				})
				.Exhaustive();
		}

		public static void CheckLifetimes(this ClassMemberSyntax member)
		{
			member.Match()
				.With<ConstructorSyntax>(constructor =>
				{
					foreach(var parameter in constructor.Parameters)
					{
						//TODO parameter.Type.DefaultOwnership(Ownership.BorrowImmutable);
					}

					throw new NotImplementedException();
				})
				.With<FieldSyntax>(field =>
				{
					throw new NotImplementedException();
				})
				.Exhaustive();
		}

		//public static Ownership CheckLifetimes(this Expression expression)
		//{
		//	return expression.Match().Returning<Ownership>()
		//		.With<NewObjectExpression>(_ => Ownership.OwnedMutable)
		//		.With<LiteralExpression>(_ => Ownership.BorrowImmutable)
		//		.Exhaustive();
		//}
	}
}
