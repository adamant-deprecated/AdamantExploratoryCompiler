using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class NewExpression : Expression
	{
		public readonly Name Type;
		public readonly IReadOnlyList<Expression> Arguments;

		public NewExpression(Name type, IEnumerable<Expression> arguments)
		{
			Requires.NotNull(type, nameof(type));

			Type = type;
			Arguments = arguments.ToList();
		}
	}
}
