using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders.LookupResults
{
	public class EmptyResult : LookupResult
	{
		public static readonly EmptyResult Instance = new EmptyResult();

		public override bool IsEmpty => true;
		public override bool IsViable => true;
		public override IEnumerable<SymbolReference> Symbols => Enumerable.Empty<SymbolReference>();

		private EmptyResult()
		{
		}

		public override LookupResult Lookup(SimpleName name, Package fromPackage)
		{
			return NotDefined();
		}
	}
}
