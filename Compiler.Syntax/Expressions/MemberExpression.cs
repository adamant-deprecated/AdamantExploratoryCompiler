using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class MemberExpression : Expression
	{
		public readonly Expression Expression;
		public readonly Token Member;

		public MemberExpression(Expression expression, Token member)
		{
			Requires.NotNull(expression, nameof(expression));
			Requires.NotNull(member, nameof(member));
			SyntaxRequires.TypeIs(member, TokenType.Identifier, nameof(member));

			Expression = expression;
			Member = member;
		}
	}
}
