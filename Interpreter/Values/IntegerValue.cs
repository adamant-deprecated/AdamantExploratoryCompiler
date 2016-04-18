using System.Numerics;

namespace Adamant.Exploratory.Interpreter.Values
{
	public class IntegerValue : Value
	{
		public readonly BigInteger Value;

		public IntegerValue(BigInteger value)
		{
			this.Value = value;
		}
	}
}
