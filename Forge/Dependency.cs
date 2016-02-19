using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Ast;

namespace Adamant.Exploratory.Forge
{
	public class Dependency
	{
		public Dependency(Assemblage assemblage, IEnumerable<string> dependencies)
		{
			Assemblage = assemblage;
			Dependencies = dependencies.ToList();
		}

		public Dependency(Assemblage assemblage)
			: this(assemblage, Enumerable.Empty<string>())
		{
		}

		public Assemblage Assemblage { get; }
		public IEnumerable<string> Dependencies { get; }
	}
}
