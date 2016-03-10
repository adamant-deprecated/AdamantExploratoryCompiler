using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class NewObjectExpression : Expression
	{
		public readonly ValueType BaseClass;
		public readonly IReadOnlyList<ValueType> Interfaces;
		public readonly IReadOnlyList<Expression> Arguments;
		public readonly IReadOnlyList<Member> Members;

		public NewObjectExpression(ValueType baseClass, IEnumerable<ValueType> interfaces, IEnumerable<Expression> arguments, IEnumerable<Member> members)
		{
			Requires.NotNull(baseClass, nameof(baseClass));

			BaseClass = baseClass;
			Interfaces = interfaces.ToList();
			Arguments = arguments.ToList();
			Members = members.ToList();
		}
	}
}
