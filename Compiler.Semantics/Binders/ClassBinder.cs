using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Semantics.References;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	internal class ClassBinder : Binder
	{
		public ClassBinder(Binder containingScope, ClassSyntax @class)
			: base(containingScope)
		{
		}

		public override IEnumerable<DeclarationReference> GetMembers(string name)
		{
			throw new NotImplementedException();
		}

		protected override LookupResult Lookup(IdentifierNameSyntax identifierName, Package fromPackage)
		{
			throw new NotImplementedException();
		}
	}
}
