using System.Text;

namespace Adamant.Exploratory.Interpreter.Values
{
	public class StringValue : Value
	{
		private readonly byte[] bytes;

		public StringValue(string value)
		{
			bytes = Encoding.UTF8.GetBytes(value);
		}

		public override Ref Access(bool isMutable, string member)
		{
			switch(member)
			{
				case "ByteLength":
					return Ref.ToField(false, new IntegerValue(bytes.Length));

				default:
					throw new InterpreterPanicException($"Member '{member}' not defined for strings");
			}
		}
	}
}
