using Adamant.Exploratory.Compiler.Syntax.Statements;

namespace Adamant.Exploratory.Compiler.Semantics.Statements
{
	public class Return : Statement
	{
		public new ReturnSyntax Syntax => (ReturnSyntax)base.Syntax;

		public Return(ReturnSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
