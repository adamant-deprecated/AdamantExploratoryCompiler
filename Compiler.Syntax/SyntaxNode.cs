namespace Adamant.Exploratory.Compiler.Syntax
{
	/// <summary>
	/// A node of the syntax tree
	/// </summary>
	public abstract class SyntaxNode
	{
		public bool IsPoisoned { get; private set; }

		public void Poison()
		{
			IsPoisoned = true;
		}
	}
}
