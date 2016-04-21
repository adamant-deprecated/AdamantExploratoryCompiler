namespace Adamant.Exploratory.Interpreter.Values
{
	public class VoidValue : Value
	{
		#region Singleton
		public static readonly VoidValue Instance = new VoidValue();

		private VoidValue()
		{
		}
		#endregion

		public override Ref Access(bool isMutable, string member)
		{
			throw new InterpreterPanicException("Can't access member of void");
		}
	}
}
