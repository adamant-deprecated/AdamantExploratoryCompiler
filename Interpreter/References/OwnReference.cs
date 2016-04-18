using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Interpreter.Values;

namespace Adamant.Exploratory.Interpreter.References
{
	public class OwnReference : Reference
	{
		private Value value;
		private BorrowReference mutableBorrow;
		private readonly List<BorrowReference> immutableBorrows = new List<BorrowReference>();
		public bool IsMutablyBorrowed => mutableBorrow != null;
		public int ImmutableBorrowCount => immutableBorrows.Count;
		public bool IsMoved => value == null;
		public bool IsStatic { get; private set; }

		public OwnReference(Value value, bool isMutable, bool isStatic = false)
			: base(isMutable)
		{
			Requires.NotNull(value, nameof(value));

			this.value = value;
			IsStatic = isStatic;
		}

		public override Value Value => value;

		public override BorrowReference Borrow(bool mutable = false)
		{
			if(IsMoved)
				throw new InterpreterPanicException("Can't borrow moved value");

			if(mutable)
			{
				if(!IsMutable)
					throw new InterpreterPanicException("Can't mutably borrow from an immutable own");

				if(mutableBorrow != null)
					throw new InterpreterPanicException("Can't mutably borrow something more than once");

				if(ImmutableBorrowCount > 0)
					throw new InterpreterPanicException("Can't mutably borrow something that is immutably borrowed");
			}

			var newReference = new BorrowReference(this, mutable);
			if(mutable)
				mutableBorrow = newReference;
			else
				immutableBorrows.Add(newReference);
			return newReference;
		}

		public override OwnReference TakeOwnership(bool mutable = false)
		{
			if(IsMoved)
				throw new InterpreterPanicException("Can't take ownership of moved value");

			if(mutableBorrow != null)
				throw new InterpreterPanicException("Can't take ownership of something that is mutably borrowed");

			if(ImmutableBorrowCount > 0)
				throw new InterpreterPanicException("Can't take ownership of something that is immutably borrowed");

			var newReference = new OwnReference(value, mutable, IsStatic);
			value = null; // moved
			return newReference;
		}

		public override void Release()
		{
			if(IsStatic) return; // Don't delete values that are actually static
			value?.Delete(); // null if moved
		}

		internal void ReleaseBorrow(BorrowReference borrowReference)
		{
			if(mutableBorrow == borrowReference)
				mutableBorrow = null;
			else
				immutableBorrows.Remove(borrowReference);
		}
	}
}
