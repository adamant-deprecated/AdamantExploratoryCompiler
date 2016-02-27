using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Syntax
{
	/// <summary>
	/// A single compilation unit i.e. a source file
	/// </summary>
	public class CompilationUnit : Node
	{
		private readonly List<Declaration> declarations;

		internal CompilationUnit(IEnumerable<Declaration> declarations)
		{
			this.declarations = declarations.ToList();
		}

		public IReadOnlyList<Declaration> Declarations => declarations;
	}
}
