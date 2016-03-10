using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class SymbolTable
	{
		public readonly PackageSymbol Package;
		public readonly IReadOnlyDictionary<SyntaxNode, Symbol> Symbols;

		internal SymbolTable(PackageSymbol package, IReadOnlyDictionary<SyntaxNode, Symbol> symbols)
		{
			Package = package;
			Symbols = symbols;
		}
	}
}
