namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class NumericType : PlainType
	{
		public readonly Token Name;

		public NumericType(Token name)
		{
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Name = name;
			// TODO really parse the type name
		}
	}
}
