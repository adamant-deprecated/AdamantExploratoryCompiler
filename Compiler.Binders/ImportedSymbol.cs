using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Binders.SymbolReferences;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class ImportedSymbol
	{
		public readonly SymbolReference Reference;
		public readonly string Alias;
		public string AliasName => Alias ?? Reference.Name;
		public readonly bool IsAlias;

		public ImportedSymbol(SymbolReference reference, string alias)
		{
			Requires.NotNull(reference, nameof(reference));
			Requires.NotEmpty(alias, nameof(alias));

			Reference = reference;
			Alias = alias;
			IsAlias = alias != null;
		}
	}
}
