using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public abstract class UsingStatementsScope: NameScope
	{
		private readonly Definitions usingDefinitions;

		protected UsingStatementsScope(IEnumerable<Definition> usingDefinitions)
		{
			this.usingDefinitions = new Definitions(usingDefinitions);
		}
	}
}
