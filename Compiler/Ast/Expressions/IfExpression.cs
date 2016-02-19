using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast.Expressions
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

		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitIf(this, param);
		}
	}
}
