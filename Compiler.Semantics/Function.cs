using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public class Function : Entity
	{
		// TODO return type

		public Function(PackageSyntax containingPackage, Accessibility declaredAccessibility, string name)
			: base(containingPackage, declaredAccessibility, name)
		{
		}
	}
}
