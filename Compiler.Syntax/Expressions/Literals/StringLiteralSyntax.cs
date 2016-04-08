namespace Adamant.Exploratory.Compiler.Syntax.Expressions.Literals
{
	public class StringLiteralSyntax : LiteralSyntax
	{
		public readonly string Text;
		public readonly string Value;

		public StringLiteralSyntax(string text)
		{
			Text = text;
			Value = text.Substring(1, text.Length - 2);// TODO handle escape sequences
		}
	}
}
