using System;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Interpreter.Values;

namespace Adamant.Exploratory.Interpreter
{
	public class Ref : IDisposable
	{
		public readonly bool IsMutable;
		private Value value;
		private readonly bool hasOwnership;
		public bool IsMoved => value == null;

		private Ref(bool mutable, Value value, bool hasOwnership)
		{
			if(!value.IsMutable && mutable)
				throw new InterpreterPanicException("Can't have a mutable reference to an immutable value");

			IsMutable = mutable;
			this.value = value;
			this.hasOwnership = hasOwnership;
		}

		public static Ref Own(Value value)
		{
			return new Ref(value.IsMutable, value, true);
		}

		public static Ref ToStatic(Value value)
		{
			return new Ref(value.IsMutable, value, false);
		}

		public static Ref ToField(bool mutable, Value value)
		{
			return new Ref(mutable, value, false);
		}

		public Ref Borrow(bool mutable = false)
		{
			if(mutable && !IsMutable)
				throw new InterpreterPanicException("Can't mutably borrow from an immutable reference");

			return new Ref(mutable, value, false);
		}

		public Ref TakeOwnership(bool mutable = false)
		{
			if(IsMoved)
				throw new InterpreterPanicException("Can't take ownership of moved value");
			if(mutable && !value.IsMutable)
				throw new InterpreterPanicException("Can't take mutable ownership of immutable value");

			// Becuase we assume the code is correct, we allow TakeOwnership() of a borrow on the assumption
			// it must be a static reference
			var newRef = new Ref(mutable, value, hasOwnership);
			value = null; // moved
			return newRef;
		}

		public void Dispose()
		{
			if(hasOwnership)
			{
				value.Delete();
				value = null;
			}
		}

		#region Value Operations

		public int AsExitCode()
		{
			return value.Match().Returning<int>()
				.With<VoidValue>(value => 0)
				.With<IntegerValue>(value => (int)value.Value)
				.Exhaustive();
		}
		#endregion

		public Ref Access(string member)
		{
			return value.Access(IsMutable, member);
		}


	}
}