namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class Member : Node
	{
		protected Member(AccessModifier access)
		{
			Access = access;
		}

		public AccessModifier Access { get; }
	}
}
