using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class Member : Node
	{
		protected Member(AccessModifier access)
		{
			Access = access;
		}

		public AccessModifier Access { get; }

		public abstract TReturn Accept<TParam, TReturn>(IMemberVisitor<TParam, TReturn> visitor, TParam param);
	}
}
