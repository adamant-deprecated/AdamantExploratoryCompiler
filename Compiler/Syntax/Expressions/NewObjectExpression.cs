using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class NewObjectExpression : Expression
	{
		public readonly Type BaseClass;
		public readonly IReadOnlyList<Type> Interfaces;
		public readonly IReadOnlyList<Expression> Arguments;
		public readonly IReadOnlyList<Member> Members;

		public NewObjectExpression(Type baseClass, IEnumerable<Type> interfaces, IEnumerable<Expression> arguments, IEnumerable<Member> members)
		{

			BaseClass = baseClass;
			Interfaces = interfaces.ToList();
			Arguments = arguments.ToList();
			Members = members.ToList();
		}

		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitNewObject(this, param);
		}
	}
}
