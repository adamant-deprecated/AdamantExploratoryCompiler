using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class GlobalSymbols
	{
		private readonly IDictionary<FullyQualifiedName, EntityDeclaration> symbols;

		public GlobalSymbols(IEnumerable<EntityDeclaration> entities)
		{
			symbols = entities.ToDictionary(d => d.FullyQualifiedName, d => d);
		}
	}
}
