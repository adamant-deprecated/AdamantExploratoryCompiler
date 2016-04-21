using System.Numerics;

namespace Adamant.Exploratory.Interpreter.Values
{
	public class IntegerValue : Value
	{
		public readonly BigInteger Value;

		public IntegerValue(BigInteger value)
		{
			Value = value;
		}

		public override Ref Access(bool isMutable, string member)
		{
			throw new System.NotImplementedException();
		}
	}
}
