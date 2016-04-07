using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class IdentifierNameSyntax : SimpleNameSyntax
	{
		public readonly SyntaxToken Identifier;
		public override TextPosition Position => Identifier.Position;

		public IdentifierNameSyntax(SyntaxToken identifier)
		{
			Requires.NotNull(identifier, nameof(identifier));
			SyntaxRequires.TypeIs(identifier, SyntaxTokenType.Identifier, nameof(identifier));

			Identifier = identifier;
		}

		public override string ToString()
		{
			return Identifier.ToString();
		}
	}
}
