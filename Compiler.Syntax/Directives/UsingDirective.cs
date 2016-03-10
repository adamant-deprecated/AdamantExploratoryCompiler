using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.Directives
{
	public class UsingDirective : SyntaxNode
	{
		public Name NamespaceOrType;

		public UsingDirective(Name namespaceOrType)
		{
			Requires.NotNull(namespaceOrType, nameof(namespaceOrType));

			NamespaceOrType = namespaceOrType;
		}
	}
}
