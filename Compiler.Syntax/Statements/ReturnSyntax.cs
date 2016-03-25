using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Statements
{
	public class ReturnSyntax : StatementSyntax
	{
		public readonly ExpressionSyntax Expression;

		public ReturnSyntax(ExpressionSyntax expression)
		{
			Requires.NotNull(expression, nameof(expression));

			Expression = expression;
		}
	}
}
