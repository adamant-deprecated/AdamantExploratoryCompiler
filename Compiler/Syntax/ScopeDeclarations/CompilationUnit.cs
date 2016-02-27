using System.Collections.Generic;

namespace Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations
{
	/// <summary>
	/// A single compilation unit i.e. a source file
	/// </summary>
	public class CompilationUnit : ScopeDeclaration
	{
		internal CompilationUnit(IEnumerable<Declaration> declarations)
			: base(null, declarations)
		{
		}
	}
}
