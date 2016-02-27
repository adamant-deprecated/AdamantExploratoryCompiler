using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class TypeName : PlainType
	{
		public TypeName(TypeName outerType, Symbol symbol)
		{
			OuterType = outerType;
			Symbol = symbol;
		}

		public TypeName OuterType { get; }
		public Symbol Symbol { get; }

		public override TReturn Accept<TParam, TReturn>(ITypeVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitTypeName(this, param);
		}
	}
}
