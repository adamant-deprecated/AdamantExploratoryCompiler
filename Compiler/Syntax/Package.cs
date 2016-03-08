using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Package
	{
		public readonly string Name;
		private readonly DefinitionCollection globalDefinitions;
		private readonly List<Package> dependencies;

		internal Package(string name, IEnumerable<Definition> globalDefinitions, IEnumerable<Package> dependencies)
		{
			Name = name;
			this.globalDefinitions = new DefinitionCollection(globalDefinitions);
			this.dependencies = dependencies.ToList();
		}

		public DefinitionCollection GlobalDefinitions => globalDefinitions;
		public IReadOnlyList<Package> Dependencies => dependencies;
	}
}
