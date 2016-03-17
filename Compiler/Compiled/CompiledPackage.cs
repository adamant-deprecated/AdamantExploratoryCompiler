using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Compiled
{
	public class CompiledPackage
	{
		public string Name;
		public readonly Package Syntax;
		public readonly PackageSymbol Symbol;

		public CompiledPackage(Package syntax, PackageSymbol symbol)
		{
			Requires.NotNull(syntax, nameof(syntax));
			Requires.NotNull(symbol, nameof(symbol));

			Name = syntax.Name;
			Syntax = syntax;
			Symbol = symbol;
		}

		// TODO dependencies
	}
}
