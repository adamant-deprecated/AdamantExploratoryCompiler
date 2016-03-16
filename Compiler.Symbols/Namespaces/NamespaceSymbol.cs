using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols.Namespaces
{
	public abstract class NamespaceSymbol : DeclarationSymbol
	{
		public bool IsGlobal => Name.Length == 0;

		protected NamespaceSymbol(PackageSymbol containingPackage, NamespaceSymbol containingNamespace, string name)
			: base(containingPackage, containingNamespace, Accessibility.Public, name) // namespaces are implicitly public
		{
		}
	}
}
