using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class UsingContext
	{
		private readonly HashSet<FullyQualifiedName> namespaces;

		public UsingContext()
		{
			namespaces = new HashSet<FullyQualifiedName>();
		}

		public UsingContext(IEnumerable<FullyQualifiedName> namespaces)
		{
			this.namespaces = new HashSet<FullyQualifiedName>(namespaces);
		}

		public UsingContext(UsingContext outerContext, IEnumerable<FullyQualifiedName> namespaces)
		{
			this.namespaces = new HashSet<FullyQualifiedName>(outerContext.namespaces.Union(namespaces));
		}
	}
}
