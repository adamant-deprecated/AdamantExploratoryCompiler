using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class CompilationUnitBuilder : Builder<CompilationUnitSyntax>
	{
		private readonly ISourceText sourceFile;
		private readonly IEnumerable<Diagnostic> errors;

		public CompilationUnitBuilder(ISourceText sourceFile, IEnumerable<Diagnostic> errors)
		{
			this.sourceFile = sourceFile;
			this.errors = errors;
		}

		public override CompilationUnitSyntax VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			// TODO global attributes
			var usingDirectives = UsingDirective(context.usingDirective());
			var declarations = context.declaration().Select(d => d.Accept(DeclarationBuilder.Instance));
			return new CompilationUnitSyntax(sourceFile, usingDirectives, declarations, errors);
		}
	}
}
