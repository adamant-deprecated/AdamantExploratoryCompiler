using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class BinaryOperatorExpression : Expression
	{
		public readonly Expression Lhs;
		public readonly Expression Rhs;

		public BinaryOperatorExpression(Expression lhs, Expression rhs)
		{
			Requires.NotNull(lhs, nameof(lhs));
			Requires.NotNull(rhs, nameof(rhs));

			Lhs = lhs;
			Rhs = rhs;
		}
	}
}
