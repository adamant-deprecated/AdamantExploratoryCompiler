using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols.Namespaces
{
	public abstract class NamespaceSymbol : DeclarationSymbol
	{
		protected NamespaceSymbol(PackageSymbol containingPackage, NamespaceSymbol containingNamespace, string name)
			: base(containingPackage, containingNamespace, Accessibility.Public, name) // namespaces are implicitly public
		{
		}

		public abstract IReadOnlyList<DeclarationSymbol> GetMembers(string name);
	}
}
