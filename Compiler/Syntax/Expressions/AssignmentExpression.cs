namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class AssignmentExpression : Expression
	{
		public readonly Expression LValue;
		public readonly Expression RValue;

		public AssignmentExpression(Expression lValue, Expression rValue)
		{
			LValue = lValue;
			RValue = rValue;
		}
	}
}
