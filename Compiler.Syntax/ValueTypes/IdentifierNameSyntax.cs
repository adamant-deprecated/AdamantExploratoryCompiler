using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class IdentifierNameSyntax : SimpleNameSyntax
	{
		public readonly Token Identifier;
		public override TextPosition Position => Identifier.Position;

		public IdentifierNameSyntax(Token identifier)
		{
			Requires.NotNull(identifier, nameof(identifier));
			SyntaxRequires.TypeIs(identifier, TokenType.Identifier, nameof(identifier));

			Identifier = identifier;
		}

		public override string ToString()
		{
			return Identifier.ToString();
		}
	}
}
