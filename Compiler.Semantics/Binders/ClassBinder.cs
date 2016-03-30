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
		public ClassBinder(Binder containingScope, ClassSyntax @class)
			: base(containingScope)
		{
		}

		public override IEnumerable<SymbolReference> GetMembers(string name)
		{
			throw new NotImplementedException();
		}

		protected override LookupResult Lookup(IdentifierNameSyntax identifierName, PackageSyntax fromPackage)
		{
			throw new NotImplementedException();
		}
	}
}
