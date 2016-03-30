using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Semantics.References;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	internal class NamespaceBinder : ContainerBinder
	{
		internal NamespaceBinder(Binder containingScope, NamespaceReference mergedContainer, IEnumerable<ImportedSymbol> imports)
			: base(containingScope, mergedContainer, imports)
		{
		}
	}
}
