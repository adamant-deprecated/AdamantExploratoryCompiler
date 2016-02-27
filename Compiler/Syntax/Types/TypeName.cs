using Adamant.Exploratory.Compiler.Symbols;

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
	}
}
