using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class BinaryOperationSyntax : ExpressionSyntax
	{
		public readonly ExpressionSyntax Lhs;
		public readonly ExpressionSyntax Rhs;

		public BinaryOperationSyntax(ExpressionSyntax lhs, ExpressionSyntax rhs)
		{
			Requires.NotNull(lhs, nameof(lhs));
			Requires.NotNull(rhs, nameof(rhs));

			Lhs = lhs;
			Rhs = rhs;
		}
	}
}
