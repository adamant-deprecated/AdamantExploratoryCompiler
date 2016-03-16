using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class IdentifierName : SimpleName
	{
		public readonly Token Identifier;
		public override TextPosition Position => Identifier.Position;

		public IdentifierName(Token identifier)
		{
			Requires.NotNull(identifier, nameof(identifier));
			SyntaxRequires.TypeIs(identifier, TokenType.Identifier, nameof(identifier));

			Identifier = identifier;
		}
	}
}
