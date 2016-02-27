using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Project
	{
		private readonly List<Project> dependencies;
		private readonly List<EntityDeclaration> entities;

		internal Project(IEnumerable<EntityDeclaration> entities, GlobalSymbols globals, IEnumerable<Project> dependencies)
		{
			this.entities = entities.ToList();
			Globals = globals;
			this.dependencies = dependencies.ToList();
		}

		public IReadOnlyList<Declaration> Entities => entities;
		public GlobalSymbols Globals { get; }
		public IReadOnlyList<Project> Dependencies => dependencies;
	}
}
