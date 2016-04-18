using System;
using Adamant.Exploratory.Interpreter.Values;

namespace Adamant.Exploratory.Interpreter.References
{
	public class BorrowReference : Reference
	{
		private readonly OwnReference ownReference;

		public BorrowReference(OwnReference ownReference, bool isMutable )
			: base( isMutable)
		{
			this.ownReference = ownReference;
		}

		public override Value Value => ownReference.Value;

		public override BorrowReference Borrow(bool mutable = false)
		{
			if(mutable && !IsMutable)
				throw new InterpreterPanicException("Can't mutably borrow from an immutable borrow");

			return ownReference.Borrow(mutable);
		}

		public override OwnReference TakeOwnership(bool mutable = false)
		{
			throw new InterpreterPanicException("Can't take ownership from a borrow reference");
		}

		public override void Release()
		{
			ownReference.ReleaseBorrow(this);
		}
	}
}
