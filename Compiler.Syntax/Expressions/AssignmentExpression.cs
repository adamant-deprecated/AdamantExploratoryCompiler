using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class AssignmentExpression : Expression
	{
		public readonly Expression LValue;
		public readonly Expression RValue;

		public AssignmentExpression(Expression lValue, Expression rValue)
		{
			Requires.NotNull(lValue, nameof(lValue));
			Requires.NotNull(rValue, nameof(rValue));

			LValue = lValue;
			RValue = rValue;
		}
	}
}
