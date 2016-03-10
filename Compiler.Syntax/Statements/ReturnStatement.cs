using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Statements
{
	public class ReturnStatement : Statement
	{
		public readonly Expression Expression;

		public ReturnStatement(Expression expression)
		{
			Requires.NotNull(expression, nameof(expression));

			Expression = expression;
		}
	}
}
