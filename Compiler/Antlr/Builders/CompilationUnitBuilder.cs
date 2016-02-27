using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class CompilationUnitBuilder : Builder<CompilationUnit>
	{
		public override CompilationUnit VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			// TODO global attributes
			var usingNames = new UsingScope(UsingNames(context.usingStatement()));
			var visitor = new DeclarationBuilder(usingNames, null);
			var declarations = context.declaration().SelectMany(d => d.Accept(visitor));
			return new CompilationUnit(declarations);
		}
	}
}
