using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class ReferenceTypeSyntax : SyntaxNode
	{
		public readonly bool? IsOwned;
		public readonly bool IsMutable;
		public readonly ValueTypeSyntax Type;

		public ReferenceTypeSyntax(bool? isOwned, bool isMutable, ValueTypeSyntax type)
		{
			Requires.NotNull(type, nameof(type));

			IsOwned = isOwned;
			IsMutable = isMutable;
			Type = type;
		}
	}
}
