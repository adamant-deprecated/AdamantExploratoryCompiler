using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public abstract class Expression : SourceSemanticNode
	{
		public new ExpressionSyntax Syntax => (ExpressionSyntax)base.Syntax;

		protected Expression(ExpressionSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
