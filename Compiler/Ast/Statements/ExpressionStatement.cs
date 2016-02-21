namespace Adamant.Exploratory.Compiler.Ast.Statements
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
