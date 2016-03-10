using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax.Directives;

namespace Adamant.Exploratory.Compiler.Syntax
{
	/// <summary>
	/// A single compilation unit i.e. a source file
	/// </summary>
	public class CompilationUnit : SyntaxNode
	{
		public readonly ISourceFile SourceFile;
		public readonly IReadOnlyList<UsingDirective> UsingDirectives;
		public readonly IReadOnlyList<Declaration> Declarations;
		public readonly IReadOnlyList<Diagnostic> Diagnostics;

		public CompilationUnit(ISourceFile sourceFile, IEnumerable<UsingDirective> usingDirectives, IEnumerable<Declaration> declarations, IEnumerable<Diagnostic> diagnostics)
		{
			SourceFile = sourceFile;
			UsingDirectives = usingDirectives.ToList();
			Declarations = declarations.ToList();
			Diagnostics = diagnostics.ToList();
		}
	}
}
