namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class BinaryOperatorExpression : Expression
	{
		public readonly Expression Lhs;
		public readonly Expression Rhs;

		public BinaryOperatorExpression(Expression lhs, Expression rhs)
		{
			Lhs = lhs;
			Rhs = rhs;
		}
	}
}
