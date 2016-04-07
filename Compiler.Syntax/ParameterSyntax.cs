using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class ParameterSyntax : SyntaxNode
	{
		public readonly SyntaxToken Name;
		public readonly ReferenceTypeSyntax Type;

		public ParameterSyntax(SyntaxToken name, ReferenceTypeSyntax type)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, SyntaxTokenType.Identifier, nameof(name));

			Name = name;
			Type = type;
		}
	}
}
