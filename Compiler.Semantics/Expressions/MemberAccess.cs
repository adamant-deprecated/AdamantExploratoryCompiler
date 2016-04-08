using Adamant.Exploratory.Compiler.Syntax.Expressions;

namespace Adamant.Exploratory.Compiler.Semantics.Expressions
{
	public class MemberAccess : Expression
	{
		public new MemberAccessSyntax Syntax => (MemberAccessSyntax)base.Syntax;
		public Expression Expression { get; }
		public string Member { get; }

		public MemberAccess(MemberAccessSyntax syntax, Package containingPackage, Expression expression)
			: base(syntax, containingPackage)
		{
			Expression = expression;
			Member = syntax.Member.ValueText;
		}
	}
}
