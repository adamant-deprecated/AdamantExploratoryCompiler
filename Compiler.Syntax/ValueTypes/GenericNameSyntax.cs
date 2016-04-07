using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class GenericNameSyntax : SimpleNameSyntax
	{
		public readonly SyntaxToken Identifier;
		public override TextPosition Position => Identifier.Position;
		// TODO generic type params

		public GenericNameSyntax(SyntaxToken identifier)
		{
			Identifier = identifier;
		}
	}
}
