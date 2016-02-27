using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class UsingNameScope
	{
		private readonly HashSet<FullyQualifiedName> namespaces;

		public UsingNameScope(IEnumerable<FullyQualifiedName> namespaces)
		{
			this.namespaces = new HashSet<FullyQualifiedName>(namespaces);
		}

		public UsingNameScope(UsingNameScope outerNameScope, IEnumerable<FullyQualifiedName> namespaces)
		{
			this.namespaces = new HashSet<FullyQualifiedName>(outerNameScope.namespaces.Union(namespaces));
		}
	}
}
