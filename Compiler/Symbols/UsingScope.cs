using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class UsingScope : NameScope
	{
		private readonly HashSet<FullyQualifiedName> namespaces;

		public UsingScope(IEnumerable<FullyQualifiedName> namespaces)
		{
			this.namespaces = new HashSet<FullyQualifiedName>(namespaces);
		}

		public UsingScope(UsingScope outerScope, IEnumerable<FullyQualifiedName> namespaces)
		{
			this.namespaces = new HashSet<FullyQualifiedName>(outerScope.namespaces.Union(namespaces));
		}

		public void BindGlobals(GlobalSymbols projectGlobals, IList<GlobalSymbols> globals)
		{
			throw new System.NotImplementedException();
		}
	}
}
