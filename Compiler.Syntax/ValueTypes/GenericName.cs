namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class GenericName : SimpleName
	{
		public readonly Token Identifier;
		// TODO generic type params

		public GenericName(Token identifier)
		{
			Identifier = identifier;
		}
	}
}
