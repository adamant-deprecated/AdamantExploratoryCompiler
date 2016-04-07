using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public class GlobalVariable : Entity
	{
		public GlobalVariable(GlobalVariableSyntax syntax, Namespace containingNamespace)
			: base(syntax, containingNamespace, syntax.Accessibility, syntax.Name.ValueText)
		{
		}
	}
}
