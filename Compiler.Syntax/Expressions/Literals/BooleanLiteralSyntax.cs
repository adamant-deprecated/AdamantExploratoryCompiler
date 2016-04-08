namespace Adamant.Exploratory.Compiler.Syntax.Expressions.Literals
{
	public class BooleanLiteralSyntax : LiteralSyntax
	{
		public readonly string Text;
		public readonly bool Value;

		public BooleanLiteralSyntax(string text)
		{
			Text = text;
			Value = bool.Parse(text);
		}
	}
}
