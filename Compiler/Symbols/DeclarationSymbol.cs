using Adamant.Exploratory.Compiler.Symbols.Namespaces;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public abstract class DeclarationSymbol : Symbol
	{
		public readonly NamespaceSymbol ContainingNamespace;

		protected DeclarationSymbol(PackageSymbol containingPackage, NamespaceSymbol containingNamespace, Accessibility declaredAccessibility, string name)
			: base(containingPackage, declaredAccessibility, name)
		{
			ContainingNamespace = containingNamespace;
		}
	}
}
