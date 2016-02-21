using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Ast
{
	/// <summary>
	/// A single compilation unit i.e. a source file
	/// </summary>
	public class CompilationUnit : Node, IDeclarationContainer
	{
		private readonly IList<Declaration> declarations;

		internal CompilationUnit(IEnumerable<IDeclarationContainer> declarationContainers)
		{
			declarations = declarationContainers.SelectMany(c => c.Declarations).ToList();
		}

		public IEnumerable<Declaration> Declarations => declarations;
	}
}
