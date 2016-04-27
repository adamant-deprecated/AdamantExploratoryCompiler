using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Semantics.References;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	internal class FunctionBinder : Binder
	{
		public FunctionBinder(Binder containingScope)
			: base(containingScope)
		{
		}

		public override IEnumerable<DeclarationReference> GetMembers(string name)
		{
			return Enumerable.Empty<DeclarationReference>();
		}

		protected override LookupResult Lookup(IdentifierNameSyntax identifierName, Package fromPackage)
		{
			throw new NotImplementedException();
		}
	}
}
