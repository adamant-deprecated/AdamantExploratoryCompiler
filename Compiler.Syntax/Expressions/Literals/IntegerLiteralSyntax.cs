using System.Numerics;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions.Literals
{
	public class IntegerLiteralSyntax : LiteralSyntax
	{
		public readonly string Text;
		public readonly BigInteger Value;

		public IntegerLiteralSyntax(string text)
		{
			Text = text;
			Value = BigInteger.Parse(text);
		}
	}
}
