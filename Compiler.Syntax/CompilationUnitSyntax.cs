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
	public class CompilationUnitSyntax : SyntaxNode
	{
		public readonly ISourceFile SourceFile;
		public readonly IReadOnlyList<UsingSyntax> UsingDirectives;
		public readonly IReadOnlyList<DeclarationSyntax> Declarations;
		public readonly IReadOnlyList<Diagnostic> Diagnostics;

		public CompilationUnitSyntax(ISourceFile sourceFile, IEnumerable<UsingSyntax> usingDirectives, IEnumerable<DeclarationSyntax> declarations, IEnumerable<Diagnostic> diagnostics)
		{
			SourceFile = sourceFile;
			UsingDirectives = usingDirectives.ToList();
			Declarations = declarations.ToList();
			Diagnostics = diagnostics.ToList();
		}
	}
}
