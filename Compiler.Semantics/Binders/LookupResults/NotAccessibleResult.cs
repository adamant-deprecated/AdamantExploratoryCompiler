using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Semantics.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults
{
	public class NotAccessibleResult : LookupResult
	{
		private readonly List<SymbolReference> symbols;

		public override bool IsEmpty => false;
		public override bool IsViable => false;
		public override IEnumerable<SymbolReference> Symbols => symbols;

		public NotAccessibleResult(IEnumerable<SymbolReference> symbols)
		{
			this.symbols = symbols.ToList();
		}

		public override LookupResult Lookup(SimpleNameSyntax name, PackageSyntax fromPackage)
		{
			throw new NotImplementedException();
		}
	}
}
