using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class PackageSymbols
	{
		public readonly PackageSymbol Package;
		public readonly IReadOnlyDictionary<SyntaxNode, Symbol> ForNode;

		internal PackageSymbols(PackageSymbol package, IReadOnlyDictionary<SyntaxNode, Symbol> forNode)
		{
			Package = package;
			ForNode = forNode;
		}
	}
}
