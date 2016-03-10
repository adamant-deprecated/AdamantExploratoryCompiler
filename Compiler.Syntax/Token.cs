using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Token
	{
		public readonly TokenType Type;
		public readonly TextPosition Position;
		public readonly string Text;

		public Token(TokenType type, TextPosition position, string text)
		{
			Requires.EnumDefined(type, nameof(type));
			Requires.NotNullOrEmpty(text, nameof(text));

			Type = type;
			Position = position;
			Text = text;
		}
	}
}
