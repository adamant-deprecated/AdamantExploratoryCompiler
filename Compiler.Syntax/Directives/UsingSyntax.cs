using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Syntax.Directives
{
	public class UsingSyntax : SyntaxNode
	{
		public NameSyntax Name;

		public UsingSyntax(NameSyntax name)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
		}
	}
}
