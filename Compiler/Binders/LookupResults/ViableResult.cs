﻿using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders.LookupResults
{
	public class ViableResult : LookupResult
	{
		private readonly List<SymbolReference> symbols;

		public override bool IsEmpty => false;
		public override bool IsViable => true;
		public override IEnumerable<SymbolReference> Symbols => symbols;

		public ViableResult(SymbolReference symbol)
		{
			Requires.NotNull(symbol, nameof(symbol));
			symbols = new List<SymbolReference>() { symbol };
		}

		public override LookupResult Lookup(SimpleName name)
		{
			return name.Match().Returning<LookupResult>()
				.With<IdentifierName>(identifierName =>
				{
					var identifier = identifierName.Identifier.ValueText;
					var members = symbols.SelectMany(r => r.Symbol.GetMembers(identifier).Select(m => CorrectReference(m, r.InSamePackage))).ToList();

					// TODO this looks like a duplicate of the Resolve method on ContainerBinder
					if(members.Count == 0)
						return Empty;

					var visible = members.Where(m => m.IsVisible).ToList();
					if(visible.Count == 1)
						return Good(visible.Single());

					var visibleInPackage = visible.Where(r => r.InSamePackage).ToList();
					if(visibleInPackage.Count == 1)
						return Good(visibleInPackage.Single()); // TODO issue warning that we have chosen the one in the current package

					if(visibleInPackage.Count > 1 || visible.Count > 1)
						return Ambiguous(visible);

					// Nothing visible in package
					if(members.Count > 0)
						return NotAccessible(members);

					return NotDefined();
				})
				.Exhaustive();
		}

		private SymbolReference CorrectReference(SymbolReference referenece, bool inSamePackage)
		{
			if(referenece.InSamePackage == inSamePackage)
				return referenece;

			return new SymbolReference(referenece.Symbol, inSamePackage);
		}
	}
}