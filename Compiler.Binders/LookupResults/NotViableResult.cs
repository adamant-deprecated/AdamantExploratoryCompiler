﻿using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders.LookupResults
{
	public class NotViableResult : LookupResult
	{
		private readonly List<SymbolReference> symbols;

		public override bool IsEmpty => false;
		public override bool IsViable => false;
		public override IEnumerable<SymbolReference> Symbols => symbols;

		public NotViableResult(IEnumerable<SymbolReference> symbols)
		{
			this.symbols = symbols.ToList();
		}

		public override LookupResult Lookup(SimpleName name, Package fromPackage)
		{
			throw new NotImplementedException();
		}
	}
}