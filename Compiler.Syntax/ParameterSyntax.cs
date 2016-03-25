using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class ParameterSyntax : SyntaxNode
	{
		public readonly Token Name;
		public readonly ReferenceTypeSyntax Type;

		public ParameterSyntax(Token name, ReferenceTypeSyntax type)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Name = name;
			Type = type;
		}
	}
}
