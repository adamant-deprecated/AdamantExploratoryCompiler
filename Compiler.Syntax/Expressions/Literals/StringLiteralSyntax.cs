namespace Adamant.Exploratory.Compiler.Syntax.Expressions.Literals
{
	public class StringLiteralSyntax : LiteralSyntax
	{
		public readonly string Text;

		public StringLiteralSyntax(string text)
		{
			Text = text;
		}
	}
}
