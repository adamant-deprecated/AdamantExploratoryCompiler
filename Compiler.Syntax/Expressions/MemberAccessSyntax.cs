using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class MemberAccessSyntax : ExpressionSyntax
	{
		public readonly ExpressionSyntax Expression;
		public readonly SyntaxToken Member;

		public MemberAccessSyntax(ExpressionSyntax expression, SyntaxToken member)
		{
			Requires.NotNull(expression, nameof(expression));
			Requires.NotNull(member, nameof(member));
			SyntaxRequires.TypeIs(member, SyntaxTokenType.Identifier, nameof(member));

			Expression = expression;
			Member = member;
		}
	}
}
