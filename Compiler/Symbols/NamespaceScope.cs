using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class NamespaceScope : UsingStatementsScope
	{
		private readonly NamespaceDefinition @namespace;

		public NamespaceScope(NamespaceDefinition @namespace, IEnumerable<Definition> usingDefinitions)
			: base(usingDefinitions)
		{
			if(@namespace == null) throw new ArgumentNullException(nameof(@namespace));
			this.@namespace = @namespace;
		}

		public override Definition LookupInScope(Symbol name)
		{
			return @namespace.Definitions.TryGetValue(name);
		}
	}
}
