using Adamant.Exploratory.Compiler.Syntax.Expressions.Literals;

namespace Adamant.Exploratory.Compiler.Semantics.Expressions.Literals
{
	public class StringLiteral : Literal
	{
		public new StringLiteralSyntax Syntax => (StringLiteralSyntax)base.Syntax;
		public string Value => Syntax.Value;

		public StringLiteral(StringLiteralSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
