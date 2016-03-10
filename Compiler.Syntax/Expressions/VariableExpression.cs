using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class VariableExpression : Expression
	{
		public readonly Name Name;

		public VariableExpression(Name name)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
		}
	}
}
