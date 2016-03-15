using Adamant.Exploratory.Compiler.Symbols.Namespaces;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class FunctionSymbol : DeclarationSymbol
	{
		public FunctionSymbol(PackageSymbol containingPackage, NamespaceSymbol containingNamespace, Accessibility declaredAccessibility, string name)
			: base(containingPackage, containingNamespace, declaredAccessibility, name)
		{
		}
	}
}
