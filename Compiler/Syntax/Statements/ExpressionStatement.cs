namespace Adamant.Exploratory.Compiler.Syntax.Statements
{
	public class ExpressionStatement : Statement
	{
		public readonly Expression Expression;

		public ExpressionStatement(Expression expression)
		{
			Expression = expression;
		}
	}
}
