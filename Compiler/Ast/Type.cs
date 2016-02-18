using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast
{
	public abstract class Type : Node
	{
		public abstract TReturn Accept<TParam, TReturn>(ITypeVisitor<TParam, TReturn> visitor, TParam param);
	}
}
