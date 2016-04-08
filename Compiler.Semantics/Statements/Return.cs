using Adamant.Exploratory.Compiler.Syntax.Statements;

namespace Adamant.Exploratory.Compiler.Semantics.Statements
{
	public class Return : Statement
	{
		public new ReturnSyntax Syntax => (ReturnSyntax)base.Syntax;
		public Expression Expression { get; set; }

		public Return(ReturnSyntax syntax, Package containingPackage, Expression expression)
			: base(syntax, containingPackage)
		{
			Expression = expression;
		}
	}
}
