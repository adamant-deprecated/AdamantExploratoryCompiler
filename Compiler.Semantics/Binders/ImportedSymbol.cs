using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics.References;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	internal class ImportedSymbol
	{
		public readonly DeclarationReference Reference;
		public readonly string Alias;
		public string AliasName => Alias ?? Reference.Name;
		public readonly bool IsAlias;

		public ImportedSymbol(DeclarationReference reference, string alias)
		{
			Requires.NotNull(reference, nameof(reference));
			Requires.NotEmpty(alias, nameof(alias));

			Reference = reference;
			Alias = alias;
			IsAlias = alias != null;
		}
	}
}
