using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public abstract class ScopeWithUsingStatements : NameScope
	{
		private readonly DefinitionCollection usingDefinitions;

		protected ScopeWithUsingStatements(IEnumerable<Definition> usingDefinitions)
		{
			this.usingDefinitions = new DefinitionCollection(usingDefinitions);
		}

		protected SymbolDefinitions LookupInUsingStatements(Symbol name, DefinitionKind kind)
		{
			throw new NotImplementedException();
		}
	}
}
