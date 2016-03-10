namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public enum Ownership
	{
		OwnedImmutable = 1,
		OwnedMutable,
		BorrowImmutable,
		BorrowMutable,
	}
}
