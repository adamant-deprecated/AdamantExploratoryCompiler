namespace Adamant.Exploratory.Compiler.Syntax.Statements
{
	public class ThrowStatement : Statement
	{
		public readonly Expression Expression;

		public ThrowStatement(Expression expression)
		{
			Expression = expression;
		}
	}
}
