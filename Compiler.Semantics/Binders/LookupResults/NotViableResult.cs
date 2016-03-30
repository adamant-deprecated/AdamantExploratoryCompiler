using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Semantics.References;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults
{
	internal class NotViableResult : LookupResult
	{
		private readonly List<DeclarationReference> symbols;

		public override bool IsEmpty => false;
		public override bool IsViable => false;
		public override IEnumerable<DeclarationReference> Symbols => symbols;

		public NotViableResult(IEnumerable<DeclarationReference> symbols)
		{
			this.symbols = symbols.ToList();
		}

		public override LookupResult Lookup(SimpleNameSyntax name, Package fromPackage)
		{
			throw new NotImplementedException();
		}
	}
}
