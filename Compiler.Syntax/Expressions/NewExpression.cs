﻿using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class NewExpression : Expression
	{
		public readonly TypeName Type;
		public readonly IReadOnlyList<Expression> Arguments;

		public NewExpression(TypeName type, IEnumerable<Expression> arguments)
		{
			Requires.NotNull(type, nameof(type));

			Type = type;
			Arguments = arguments.ToList();
		}
	}
}