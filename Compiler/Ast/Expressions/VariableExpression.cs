using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast.Expressions
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
