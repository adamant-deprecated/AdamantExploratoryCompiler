using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class CallExpression : Expression
	{
		public readonly Expression Expression;
		public readonly IReadOnlyList<Expression> Arguments;

		public CallExpression(Expression expression, IEnumerable<Expression> arguments)
		{
			Requires.NotNull(expression, nameof(expression));

			Expression = expression;
			Arguments = arguments.ToList();
		}
	}
}
