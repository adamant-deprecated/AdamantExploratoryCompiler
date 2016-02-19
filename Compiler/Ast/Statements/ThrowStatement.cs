namespace Adamant.Exploratory.Compiler.Ast.Statements
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
