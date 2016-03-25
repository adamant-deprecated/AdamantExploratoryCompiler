using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class CallSyntax : ExpressionSyntax
	{
		public readonly ExpressionSyntax Expression;
		public readonly IReadOnlyList<ExpressionSyntax> Arguments;

		public CallSyntax(ExpressionSyntax expression, IEnumerable<ExpressionSyntax> arguments)
		{
			Requires.NotNull(expression, nameof(expression));

			Expression = expression;
			Arguments = arguments.ToList();
		}
	}
}
