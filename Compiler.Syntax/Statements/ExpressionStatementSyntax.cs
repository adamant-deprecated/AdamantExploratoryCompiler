using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Statements
{
	/// <summary>
	/// An expression used as a statement, only certian expressions are allowed as statements
	/// </summary>
	public class ExpressionStatementSyntax : StatementSyntax
	{
		public readonly ExpressionSyntax Expression;

		public ExpressionStatementSyntax(ExpressionSyntax expression)
		{
			Requires.NotNull(expression, nameof(expression));

			Expression = expression;
		}
	}
}
