using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class CompilationUnitBuilder : Builder<CompilationUnit>
	{
		private readonly ISourceFile sourceFile;
		private readonly IEnumerable<Diagnostic> errors;

		public CompilationUnitBuilder(ISourceFile sourceFile, IEnumerable<Diagnostic> errors)
		{
			this.sourceFile = sourceFile;
			this.errors = errors;
		}

		public override CompilationUnit VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			// TODO global attributes
			var usingDirectives = UsingDirective(context.usingDirective());
			var visitor = new DeclarationBuilder();
			var declarations = context.declaration().Select(d => d.Accept(visitor));
			return new CompilationUnit(sourceFile, usingDirectives, declarations, errors);
		}
	}
}
