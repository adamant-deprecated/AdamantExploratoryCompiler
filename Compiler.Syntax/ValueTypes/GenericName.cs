using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class GenericName : SimpleName
	{
		public readonly Token Identifier;
		public override TextPosition Position => Identifier.Position;
		// TODO generic type params

		public GenericName(Token identifier)
		{
			Identifier = identifier;
		}
	}
}
