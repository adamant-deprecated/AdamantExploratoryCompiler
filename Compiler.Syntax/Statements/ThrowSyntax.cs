using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Statements
{
	public class ThrowSyntax : StatementSyntax
	{
		public readonly ExpressionSyntax Expression;

		public ThrowSyntax(ExpressionSyntax expression)
		{
			Requires.NotNull(expression, nameof(expression));

			Expression = expression;
		}
	}
}
