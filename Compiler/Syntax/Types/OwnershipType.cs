namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class OwnershipType : Type
	{
		public OwnershipType(Ownership ownership, PlainType type)
		{
			Ownership = ownership;
			Type = type;
		}

		public Ownership Ownership { get; private set; }
		public PlainType Type { get; }
	}
}
