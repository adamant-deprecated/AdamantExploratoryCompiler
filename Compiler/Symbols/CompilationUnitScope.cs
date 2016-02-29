using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class CompilationUnitScope : UsingStatementsScope
	{
		private readonly GlobalScope globalScope;

		public CompilationUnitScope(GlobalScope globalScope, IEnumerable<Definition> usingDefinitions)
			: base(usingDefinitions)
		{
			this.globalScope = globalScope;
		}

		public override Definition LookupInScope(Symbol name)
		{
			return globalScope.LookupInScope(name);
		}
	}
}
