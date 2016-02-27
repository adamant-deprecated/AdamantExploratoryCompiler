namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public enum Ownership
	{
		Inferred = 0,
		Owned = 1,
		MutableBorrow,
		ImmutableBorrow,
	}
}
