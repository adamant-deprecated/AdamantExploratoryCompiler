using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Project
	{
		public readonly string Name;
		private readonly DefinitionCollection globalDefinitions;
		private readonly List<Project> dependencies;

		// TODO in keeping with the idea that a project is now just syntax, we should describe the contents of the project.vson not reference other projects etc.
		internal Project(string name, IEnumerable<Definition> globalDefinitions, IEnumerable<Project> dependencies)
		{
			Name = name;
			this.globalDefinitions = new DefinitionCollection(globalDefinitions);
			this.dependencies = dependencies.ToList();
		}

		public DefinitionCollection GlobalDefinitions => globalDefinitions;
		public IReadOnlyList<Project> Dependencies => dependencies;
	}
}
