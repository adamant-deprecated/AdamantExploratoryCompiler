using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast.Expressions
{
	public class MemberExpression : Expression
	{
		public readonly Expression Expression;
		public readonly string Member;

		public MemberExpression(Expression expression, string member)
		{
			Expression = expression;
			Member = member;
		}

		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitMember(this, param);
		}
	}
}
