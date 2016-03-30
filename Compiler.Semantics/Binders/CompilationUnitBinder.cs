using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class CompilationUnitBinder : ContainerBinder
	{
		private readonly CompilationUnitSyntax compilationUnit;

		public CompilationUnitBinder(PackageBinder containingScope, CompilationUnitSyntax compilationUnit, IEnumerable<ImportedSymbol> imports)
			: base(containingScope, null, imports)
		{
			this.compilationUnit = compilationUnit;
		}

		public override IEnumerable<SymbolReference> GetMembers(string name)
		{
			return ContainingScope.GetMembers(name);
		}
	}
}
