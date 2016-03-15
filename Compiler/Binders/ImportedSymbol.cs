using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class ImportedSymbol
	{
		public readonly Symbol Symbol;
		public readonly string Alias;
		public string AliadName => Alias ?? Symbol.Name;
		public readonly bool IsAlias;
		public readonly bool IsSamePackage;

		public ImportedSymbol(Symbol symbol, string alias, bool isSamePackage)
		{
			Requires.NotNull(symbol, nameof(symbol));
			Requires.NotEmpty(alias, nameof(alias));

			Symbol = symbol;
			Alias = alias;
			IsAlias = alias != null;
			IsSamePackage = isSamePackage;
		}
	}
}
