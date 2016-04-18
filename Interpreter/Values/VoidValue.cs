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
	}
}
