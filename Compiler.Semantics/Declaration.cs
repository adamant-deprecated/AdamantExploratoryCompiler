using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public abstract class Declaration : Symbol
	{
		protected Declaration(PackageSyntax containingPackage, Accessibility declaredAccessibility, string name)
			: base(containingPackage, declaredAccessibility, name)
		{
		}
	}
}
