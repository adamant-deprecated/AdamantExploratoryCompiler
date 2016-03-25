using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders.LookupResults
{
	/// <summary>
	/// Possible kinds of lookup results:
	///		empty
	///		viable
	///		not accessible
	///		non-viable
	/// </summary>
	public abstract class LookupResult
	{
		public static readonly LookupResult Empty = EmptyResult.Instance;

		public abstract bool IsEmpty { get; }
		public abstract bool IsViable { get; }
		public abstract IEnumerable<SymbolReference> Symbols { get; }

		public abstract LookupResult Lookup(SimpleNameSyntax name, Package fromPackage);

		public static LookupResult Good(SymbolReference symbol)
		{
			Requires.NotNull(symbol, nameof(symbol));
			return new ViableResult(symbol);
		}

		public static LookupResult NotAccessible(IEnumerable<SymbolReference> symbols)
		{
			return new NotAccessibleResult(symbols);
		}

		public static LookupResult NotDefined()
		{
			return new NotViableResult(Enumerable.Empty<SymbolReference>()); // TODO include actual error
		}

		public static LookupResult Ambiguous(IEnumerable<SymbolReference> symbols)
		{
			return new NotViableResult(symbols); // TODO include actual error
		}
	}
}
