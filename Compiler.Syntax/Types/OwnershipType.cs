using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class OwnershipType : Type
	{
		public readonly Ownership Ownership;
		public readonly PlainType Type;

		public OwnershipType(Ownership ownership, PlainType type)
		{
			Requires.EnumDefined(ownership, nameof(ownership));
			Requires.NotNull(type, nameof(type));

			Ownership = ownership;
			Type = type;
		}
	}
}
