using Adamant.Exploratory.Compiler.Syntax.Statements;

namespace Adamant.Exploratory.Compiler.Semantics.Statements
{
	public class ExpressionStatement : Statement
	{
		public new ExpressionStatementSyntax Syntax => (ExpressionStatementSyntax)base.Syntax;
		public Expression Expression { get; }

		public ExpressionStatement(ExpressionStatementSyntax syntax, Package containingPackage, Expression expression)
			: base(syntax, containingPackage)
		{
			Expression = expression;
		}
	}
}
