using System.Linq;
using Adamant.Exploratory.Compiler.Semantics.Types;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public class Function : Entity
	{
		public new FunctionSyntax Syntax => (FunctionSyntax)base.Syntax.SingleOrDefault();
		public ReferenceType ReturnType { get; set; }

		public Function(FunctionSyntax syntax, Namespace containingNamespace, Accessibility declaredAccessibility, string name)
			: base(syntax, containingNamespace, declaredAccessibility, name)
		{
		}
	}
}
