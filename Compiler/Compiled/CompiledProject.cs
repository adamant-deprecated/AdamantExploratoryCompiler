using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Compiled
{
	public class CompiledPackage
	{
		public string Name;
		public readonly Package Package;
		public readonly PackageSymbol Symbol;
		public readonly IReadOnlyDictionary<SyntaxNode, Symbol> Symbols;

		public CompiledPackage(Package package, PackageSymbol symbol, IReadOnlyDictionary<SyntaxNode, Symbol> symbols)
		{
			Requires.NotNull(package, nameof(package));
			Requires.NotNull(symbol, nameof(symbol));

			Name = package.Name;
			Package = package;
			Symbol = symbol;
			Symbols = symbols;
		}

		// TODO dependencies
	}
}
