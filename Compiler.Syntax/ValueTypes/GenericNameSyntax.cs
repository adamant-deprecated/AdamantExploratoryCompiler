using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class GenericNameSyntax : SimpleNameSyntax
	{
		public readonly Token Identifier;
		public override TextPosition Position => Identifier.Position;
		// TODO generic type params

		public GenericNameSyntax(Token identifier)
		{
			Identifier = identifier;
		}
	}
}
