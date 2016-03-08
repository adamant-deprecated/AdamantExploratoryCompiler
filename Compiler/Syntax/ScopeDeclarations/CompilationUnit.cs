using System.Collections.Generic;

namespace Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations
{
	/// <summary>
	/// A single compilation unit i.e. a source file
	/// </summary>
	public class CompilationUnit : BlockWithUsingStatements
	{
		internal CompilationUnit(IEnumerable<UsingStatement> usingStatements, IEnumerable<Declaration> declarations)
			: base(usingStatements, declarations)
		{
		}
	}
}
