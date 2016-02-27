using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class UsingContext
	{
		private readonly HashSet<QualifiedName> namespaces;

		public UsingContext()
		{
			namespaces = new HashSet<QualifiedName>();
		}

		public UsingContext(IEnumerable<QualifiedName> namespaces)
		{
			this.namespaces = new HashSet<QualifiedName>(namespaces);
		}

		public UsingContext(UsingContext outerContext, IEnumerable<QualifiedName> namespaces)
		{
			this.namespaces = new HashSet<QualifiedName>(outerContext.namespaces.Union(namespaces));
		}
	}
}
