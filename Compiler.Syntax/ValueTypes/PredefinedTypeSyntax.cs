using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class PredefinedTypeSyntax : SimpleNameSyntax
	{
		public readonly Token Name;
		public override TextPosition Position => Name.Position;

		public PredefinedTypeSyntax(Token name)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.PredefinedType, nameof(name));

			Name = name;
			// TODO really parse the type name
		}
	}
}
