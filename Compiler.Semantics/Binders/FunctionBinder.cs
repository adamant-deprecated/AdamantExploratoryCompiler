using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Semantics.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	public class FunctionBinder : Binder
	{
		public FunctionBinder(Binder containingScope)
			: base(containingScope)
		{
		}

		public override IEnumerable<SymbolReference> GetMembers(string name)
		{
			return Enumerable.Empty<SymbolReference>();
		}

		protected override LookupResult Lookup(IdentifierNameSyntax identifierName, PackageSyntax fromPackage)
		{
			throw new NotImplementedException();
		}
	}
}
