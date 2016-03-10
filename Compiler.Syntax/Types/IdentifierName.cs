namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class IdentifierName : SimpleName
	{
		public readonly Token Identifier;

		public IdentifierName(Token identifier)
		{
			SyntaxRequires.TypeIs(identifier, TokenType.Identifier, nameof(identifier));

			Identifier = identifier;
		}
	}
}
