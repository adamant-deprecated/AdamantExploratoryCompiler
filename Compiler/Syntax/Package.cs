using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Package
	{
		private readonly List<Package> dependencies;
		private readonly List<Definition> globalDefinitions;

		internal Package(IEnumerable<Definition> globalDefinitions, IEnumerable<Package> dependencies)
		{
			this.globalDefinitions = globalDefinitions.ToList();
			this.dependencies = dependencies.ToList();
		}

		public IReadOnlyList<Definition> GlobalDefinitions => globalDefinitions;
		public IReadOnlyList<Package> Dependencies => dependencies;
	}
}
