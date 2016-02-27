using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class GlobalSymbols
	{
		private readonly IDictionary<FullyQualifiedName, Declaration> symbols;

		public GlobalSymbols(IEnumerable<Declaration> declarations)
		{
			symbols = declarations.ToDictionary(d => d.FullyQualifiedName, d => d);
		}
	}
}
