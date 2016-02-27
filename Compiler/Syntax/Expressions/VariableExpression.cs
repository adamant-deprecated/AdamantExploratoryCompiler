using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class VariableExpression : Expression
	{
		public readonly Name Name;

		public VariableExpression(Name name)
		{
			Name = name;
		}

		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitVariable(this, param);
		}
	}
}
