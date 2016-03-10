using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class IfExpression : Expression
	{
		public readonly Expression Condition;
		public readonly Expression Then;
		public readonly Expression Else;

		public IfExpression(Expression condition, Expression then, Expression @else)
		{
			Requires.NotNull(condition, nameof(condition));
			Requires.NotNull(then, nameof(then));
			Requires.NotNull(@else, nameof(@else));

			Condition = condition;
			Then = then;
			Else = @else;
		}
	}
}
