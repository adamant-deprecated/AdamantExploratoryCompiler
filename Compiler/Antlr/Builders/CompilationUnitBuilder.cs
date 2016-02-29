using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class CompilationUnitBuilder : Builder<CompilationUnit>
	{
		public override CompilationUnit VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			// TODO global attributes
			var usingStatements = UsingStatements(context.usingStatement());
			var visitor = new DeclarationBuilder(null);
			var declarations = context.declaration().Select(d => d.Accept(visitor));
			return new CompilationUnit(usingStatements, declarations);
		}
	}
}
