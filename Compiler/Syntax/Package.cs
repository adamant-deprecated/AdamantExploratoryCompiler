using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Package
	{
		public readonly string Name;
		private readonly Definitions globalDefinitions;
		private readonly List<Package> dependencies;

		internal Package(string name, IEnumerable<Definition> globalDefinitions, IEnumerable<Package> dependencies)
		{
			Name = name;
			this.globalDefinitions = new Definitions(globalDefinitions);
			this.dependencies = dependencies.ToList();
		}

		public Definitions GlobalDefinitions => globalDefinitions;
		public IReadOnlyList<Package> Dependencies => dependencies;
	}
}
