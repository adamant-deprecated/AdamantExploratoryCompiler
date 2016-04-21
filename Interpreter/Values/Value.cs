namespace Adamant.Exploratory.Interpreter.Values
{
	public abstract class Value
	{
		public virtual bool IsMutable => false;

		public void Delete()
		{
			throw new System.NotImplementedException();
		}

		public abstract Ref Access(bool isMutable, string member);
	}
}
