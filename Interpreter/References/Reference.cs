using Adamant.Exploratory.Interpreter.Values;

namespace Adamant.Exploratory.Interpreter.References
{
	public abstract class Reference
	{
		public abstract Value Value { get; }
		public readonly bool IsMutable;

		protected Reference(bool isMutable)
		{
			IsMutable = isMutable;
		}

		public abstract BorrowReference Borrow(bool mutable = false);
		public abstract OwnReference TakeOwnership(bool mutable = false);
		public abstract void Release();
	}
}