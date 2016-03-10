using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class NumericType : ValueType
	{
		public readonly Token Name;

		public NumericType(Token name)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Name = name;
			// TODO really parse the type name
		}
	}
}
