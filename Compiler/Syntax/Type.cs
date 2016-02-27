using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class Type : Node
	{
		public abstract TReturn Accept<TParam, TReturn>(ITypeVisitor<TParam, TReturn> visitor, TParam param);
	}
}
