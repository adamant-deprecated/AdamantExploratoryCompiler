using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class MemberAccessSyntax : ExpressionSyntax
	{
		public readonly ExpressionSyntax Expression;
		public readonly Token Member;

		public MemberAccessSyntax(ExpressionSyntax expression, Token member)
		{
			Requires.NotNull(expression, nameof(expression));
			Requires.NotNull(member, nameof(member));
			SyntaxRequires.TypeIs(member, TokenType.Identifier, nameof(member));

			Expression = expression;
			Member = member;
		}
	}
}
