namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class IfExpression : Expression
	{
		public readonly Expression Condition;
		public readonly Expression Then;
		public readonly Expression Else;

		public IfExpression(Expression condition, Expression then, Expression @else)
		{
			Condition = condition;
			Then = then;
			Else = @else;
		}
	}
}
