using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class CompilationUnitScope : ScopeWithUsingStatements
	{
		private readonly GlobalScope globalScope;

		public CompilationUnitScope(GlobalScope globalScope, IEnumerable<Definition> usingDefinitions)
			: base(usingDefinitions)
		{
			if(globalScope == null) throw new ArgumentNullException(nameof(globalScope));
			this.globalScope = globalScope;
		}

		public override GlobalScope Globals => globalScope;

		public override SymbolDefinitions Lookup(Symbol name, DefinitionKind kind = DefinitionKind.Any)
		{
			var usingDefinitions = LookupInUsingStatements(name, kind);
			if(usingDefinitions.HasAccessibleDefinitions())
				return usingDefinitions;

			var globalDefinitions = globalScope.Lookup(name, kind);
			return globalDefinitions.HasAccessibleDefinitions() || usingDefinitions.Count == 0
				? globalDefinitions
				: usingDefinitions;
		}

		public override Definition LookupInCurrentScopeOnly(Symbol name, DefinitionKind kind = DefinitionKind.Any)
		{
			return globalScope.LookupInCurrentScopeOnly(name, kind);
		}
	}
}
