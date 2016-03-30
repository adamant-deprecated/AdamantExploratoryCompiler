using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public class Class : Entity
	{
		public Class(PackageSyntax containingPackage,  Accessibility declaredAccessibility, string name)
			: base(containingPackage, declaredAccessibility, name)
		{
		}
	}
}
