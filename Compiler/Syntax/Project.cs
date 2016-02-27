using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Project
	{
		private readonly List<Project> dependencies;
		private readonly List<Declaration> declarations;

		internal Project(IEnumerable<CompilationUnit> compilationUnits, IEnumerable<Project> dependencies)
		{
			this.dependencies = dependencies.ToList();
			declarations = compilationUnits.SelectMany(c => c.Declarations).ToList();
		}

		public IReadOnlyList<Declaration> Declarations => declarations;
		public IReadOnlyList<Project> Dependencies => dependencies;
	}
}
