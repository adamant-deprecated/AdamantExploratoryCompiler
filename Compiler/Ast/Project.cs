using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Ast
{
	public class Project : IDeclarationContainer
	{
		private readonly IList<Declaration> declarations;

		internal Project(IEnumerable<CompilationUnit> compilationUnits)
		{
			declarations = compilationUnits.SelectMany(c => c.Declarations).ToList();
		}

		public IEnumerable<Declaration> Declarations => declarations;
	}
}
