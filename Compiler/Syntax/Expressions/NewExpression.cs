using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Types;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class NewExpression : Expression
	{
		public readonly TypeName Type;
		public readonly IReadOnlyList<Expression> Arguments;

		public NewExpression(TypeName type, IEnumerable<Expression> arguments)
		{
			Type = type;
			Arguments = arguments.ToList();
		}

		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitNew(this, param);
		}
	}
}
