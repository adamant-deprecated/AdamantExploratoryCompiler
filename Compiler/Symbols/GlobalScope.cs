using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class GlobalScope : NameScope
	{
		private readonly Definitions globalDefinitions;
		private readonly List<Package> dependencies;

		public GlobalScope(Definitions globalDefinitions, IEnumerable<Package> dependencies)
		{
			this.globalDefinitions = globalDefinitions;
			this.dependencies = dependencies.ToList();
		}

		public override Definition LookupInScope(Symbol name)
		{
			return globalDefinitions.TryGetValue(name);
		}
	}
}
