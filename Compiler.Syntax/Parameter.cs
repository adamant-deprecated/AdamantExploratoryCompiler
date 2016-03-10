using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Parameter : SyntaxNode
	{
		public readonly Token Name;
		public readonly ReferenceType Type;

		public Parameter(Token name, ReferenceType type)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Name = name;
			Type = type;
		}
	}
}
