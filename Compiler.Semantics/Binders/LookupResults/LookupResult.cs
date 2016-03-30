using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics.References;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Binders.LookupResults
{
	/// <summary>
	/// Possible kinds of lookup results:
	///		empty
	///		viable
	///		not accessible
	///		non-viable
	/// </summary>
	internal abstract class LookupResult
	{
		public static readonly LookupResult Empty = EmptyResult.Instance;

		public abstract bool IsEmpty { get; }
		public abstract bool IsViable { get; }
		public abstract IEnumerable<DeclarationReference> Symbols { get; }

		public abstract LookupResult Lookup(SimpleNameSyntax name, Package fromPackage);

		public static LookupResult Good(DeclarationReference declaration)
		{
			Requires.NotNull(declaration, nameof(declaration));
			return new ViableResult(declaration);
		}

		public static LookupResult NotAccessible(IEnumerable<DeclarationReference> symbols)
		{
			return new NotAccessibleResult(symbols);
		}

		public static LookupResult NotDefined()
		{
			return new NotViableResult(Enumerable.Empty<DeclarationReference>()); // TODO include actual error
		}

		public static LookupResult Ambiguous(IEnumerable<DeclarationReference> symbols)
		{
			return new NotViableResult(symbols); // TODO include actual error
		}
	}
}
