using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class TypeName : PlainType
	{
		public TypeName(TypeName outerType, Name name)
		{
			OuterType = outerType;
			Name = name;
		}

		public TypeName OuterType { get; }
		public Name Name { get; }

		public override TReturn Accept<TParam, TReturn>(ITypeVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitTypeName(this, param);
		}
	}
}
