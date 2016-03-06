using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class NamespaceScope : UsingStatementsScope
	{
		private readonly NamespaceDefinition @namespace;
		private readonly UsingStatementsScope containingScope;

		public NamespaceScope(NamespaceDefinition @namespace, IEnumerable<Definition> usingDefinitions, UsingStatementsScope containingScope)
			: base(usingDefinitions)
		{
			if(@namespace == null) throw new ArgumentNullException(nameof(@namespace));
			if(containingScope == null) throw new ArgumentNullException(nameof(containingScope));
			this.@namespace = @namespace;
			this.containingScope = containingScope;
		}

		public override GlobalScope Globals => containingScope.Globals;

		public override Definition LookupLocal(Symbol name)
		{
			return @namespace.Definitions.TryGetValue(name);
		}

		public override SymbolDefinitions Lookup(Symbol name)
		{
			Definition definition;
			if(@namespace.Definitions.TryGetValue(name, out definition))
				return new SymbolDefinitions(name, new SymbolDefinition(definition, true));

			var usingDefinitions = LookupInUsingStatements(name);
			if(usingDefinitions.HasAccessibleDefinitions())
				return usingDefinitions;

			var containingDefinitions = containingScope.Lookup(name);
			return containingDefinitions.HasAccessibleDefinitions() || usingDefinitions.Count == 0
				? containingDefinitions
				: usingDefinitions;
		}
	}
}
