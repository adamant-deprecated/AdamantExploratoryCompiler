using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast.Expressions
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

		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitBinaryOperator(this, param);
		}
	}
}
