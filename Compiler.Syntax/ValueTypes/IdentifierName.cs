using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class IdentifierName : SimpleName
	{
		public readonly Token Identifier;

		public IdentifierName(Token identifier)
		{
			Requires.NotNull(identifier, nameof(identifier));
			SyntaxRequires.TypeIs(identifier, TokenType.Identifier, nameof(identifier));

			Identifier = identifier;
		}
	}
}
