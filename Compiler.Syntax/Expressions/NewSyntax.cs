using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class NewSyntax : ExpressionSyntax
	{
		public readonly NameSyntax Type;
		public readonly IReadOnlyList<ExpressionSyntax> Arguments;

		public NewSyntax(NameSyntax type, IEnumerable<ExpressionSyntax> arguments)
		{
			Requires.NotNull(type, nameof(type));

			Type = type;
			Arguments = arguments.ToList();
		}
	}
}
