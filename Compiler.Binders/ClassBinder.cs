using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class ClassBinder : Binder
	{
		public ClassBinder(Binder containingScope, ClassDeclaration @class)
			: base(containingScope)
		{
		}

		public override IEnumerable<SymbolReference> GetMembers(string name)
		{
			throw new NotImplementedException();
		}

		protected override LookupResult Lookup(IdentifierName identifierName, Package fromPackage)
		{
			throw new NotImplementedException();
		}
	}
}
