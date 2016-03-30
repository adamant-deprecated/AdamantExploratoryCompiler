using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Semantics.References;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults
{
	internal class EmptyResult : LookupResult
	{
		public static readonly EmptyResult Instance = new EmptyResult();

		public override bool IsEmpty => true;
		public override bool IsViable => true;
		public override IEnumerable<DeclarationReference> Symbols => Enumerable.Empty<DeclarationReference>();

		private EmptyResult()
		{
		}

		public override LookupResult Lookup(SimpleNameSyntax name, Package fromPackage)
		{
			return NotDefined();
		}
	}
}
