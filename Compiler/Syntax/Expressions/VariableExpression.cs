using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class VariableExpression : Expression
	{
		public readonly Symbol Symbol;

		public VariableExpression(Symbol symbol)
		{
			Symbol = symbol;
		}
	}
}
