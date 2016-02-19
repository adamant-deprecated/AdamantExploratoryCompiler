using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast.Expressions
{
	public class CallExpression : Expression
	{
		public readonly Expression Expression;
		public readonly IReadOnlyList<Expression> Arguments;

		public CallExpression(Expression expression, IEnumerable<Expression> arguments)
		{
			Expression = expression;
			Arguments = arguments.ToList();
		}

		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitCall(this, param);
		}
	}
}
