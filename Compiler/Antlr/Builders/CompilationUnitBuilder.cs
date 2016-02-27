using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class CompilationUnitBuilder : Builder<CompilationUnit>
	{
		public override CompilationUnit VisitCompilationUnit(AdamantParser.CompilationUnitContext context)
		{
			// TODO global attributes
			var newContext = new UsingContext(GetNamespaces(context.usingStatement()));
			var visitor = new DeclarationBuilder(newContext, QualifiedName.None);
			var declarations = context.declaration().SelectMany(d => d.Accept(visitor));
			return new CompilationUnit(declarations);
		}
	}
}
