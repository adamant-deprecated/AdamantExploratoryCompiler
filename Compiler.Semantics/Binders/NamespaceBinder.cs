using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Semantics.Binders.SymbolReferences;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	public class NamespaceBinder : ContainerBinder
	{
		internal NamespaceBinder(Binder containingScope, NamespaceReference mergedContainer, IEnumerable<ImportedSymbol> imports)
			: base(containingScope, mergedContainer, imports)
		{
		}
	}
}
