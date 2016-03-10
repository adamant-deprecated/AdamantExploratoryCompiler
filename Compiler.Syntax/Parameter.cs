using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Parameter : SyntaxNode
	{
		public readonly Token Name;
		public readonly Type Type;

		public Parameter(Token name, Type type)
		{
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));
			Requires.NotNull(type, nameof(type));

			Name = name;
			Type = type;
		}
	}
}
