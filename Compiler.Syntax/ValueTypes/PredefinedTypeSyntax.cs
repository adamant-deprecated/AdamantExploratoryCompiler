using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class PredefinedTypeSyntax : SimpleNameSyntax
	{
		public readonly SyntaxToken Name;
		public override TextPosition Position => Name.Position;

		public PredefinedTypeSyntax(SyntaxToken name)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, SyntaxTokenType.PredefinedType, nameof(name));

			Name = name;
			// TODO really parse the type name
		}
	}
}
