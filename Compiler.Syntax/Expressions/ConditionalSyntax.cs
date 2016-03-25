using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	/// <summary>
	/// Syntax for the ternary conditional operator `condition ? then : else`
	/// </summary>
	public class ConditionalSyntax : ExpressionSyntax
	{
		public readonly ExpressionSyntax Condition;
		public readonly ExpressionSyntax Then;
		public readonly ExpressionSyntax Else;

		public ConditionalSyntax(ExpressionSyntax condition, ExpressionSyntax then, ExpressionSyntax @else)
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
