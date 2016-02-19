using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Ast.Types;
using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast.Expressions
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
