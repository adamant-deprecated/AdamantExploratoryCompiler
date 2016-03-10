using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Statements
{
	public class ExpressionStatement : Statement
	{
		public readonly Expression Expression;

		public ExpressionStatement(Expression expression)
		{
			Requires.NotNull(expression, nameof(expression));

			Expression = expression;
		}
	}
}
