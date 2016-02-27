namespace Adamant.Exploratory.Compiler.Syntax.Expressions
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
	}
}
