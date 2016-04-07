using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public abstract class Statement : SourceSemanticNode
	{
		public new StatementSyntax Syntax => (StatementSyntax)base.Syntax;

		protected Statement(StatementSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
