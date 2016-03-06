using System;
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
			if(globalScope == null) throw new ArgumentNullException(nameof(globalScope));
			this.globalScope = globalScope;
		}

		public override GlobalScope Globals => globalScope;

		public override Definition LookupLocal(Symbol name)
		{
			return globalScope.LookupLocal(name);
		}

		public override SymbolDefinitions Lookup(Symbol name)
		{
			var usingDefinitions = LookupInUsingStatements(name);
			if(usingDefinitions.HasAccessibleDefinitions())
				return usingDefinitions;

			var globalDefinitions = globalScope.Lookup(name);
			return globalDefinitions.HasAccessibleDefinitions() || usingDefinitions.Count == 0
				? globalDefinitions
				: usingDefinitions;
		}
	}
}
