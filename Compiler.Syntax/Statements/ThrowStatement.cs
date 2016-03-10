using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Statements
{
	public class ThrowStatement : Statement
	{
		public readonly Expression Expression;

		public ThrowStatement(Expression expression)
		{
			Requires.NotNull(expression, nameof(expression));

			Expression = expression;
		}
	}
}
