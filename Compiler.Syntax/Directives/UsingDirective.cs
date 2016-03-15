using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Syntax.Directives
{
	public class UsingDirective : SyntaxNode
	{
		public Name Name;

		public UsingDirective(Name name)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
		}
	}
}
