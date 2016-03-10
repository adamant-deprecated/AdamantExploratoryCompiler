using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class ReferenceType : SyntaxNode
	{
		public readonly bool? IsOwned;
		public readonly bool IsMutable;
		public readonly ValueType Type;

		public ReferenceType(bool? isOwned, bool isMutable, ValueType type)
		{
			Requires.NotNull(type, nameof(type));

			IsOwned = isOwned;
			IsMutable = isMutable;
			Type = type;
		}
	}
}
