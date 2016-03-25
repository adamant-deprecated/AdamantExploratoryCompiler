using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class AssignmentSyntax : ExpressionSyntax
	{
		public readonly ExpressionSyntax LValue;
		public readonly ExpressionSyntax RValue;

		public AssignmentSyntax(ExpressionSyntax lValue, ExpressionSyntax rValue)
		{
			Requires.NotNull(lValue, nameof(lValue));
			Requires.NotNull(rValue, nameof(rValue));

			LValue = lValue;
			RValue = rValue;
		}
	}
}
