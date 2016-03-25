using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	// TODO merge with StringTypeSyntax and rename to PredefinedTypeSyntax
	public class NumericTypeSyntax : ValueTypeSyntax
	{
		public readonly Token Name;

		public NumericTypeSyntax(Token name)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Name = name;
			// TODO really parse the type name
		}
	}
}
