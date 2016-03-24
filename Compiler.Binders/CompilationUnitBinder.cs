using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class CompilationUnitBinder : ContainerBinder
	{
		private readonly CompilationUnit compilationUnit;

		public CompilationUnitBinder(PackageBinder containingScope, CompilationUnit compilationUnit, IEnumerable<ImportedSymbol> imports)
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
