using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public class Class : Entity
	{
		public new IEnumerable<ClassSyntax> Syntax => base.Syntax.Cast<ClassSyntax>();

		public Class(ClassSyntax syntax, Namespace containingNamespace, Accessibility declaredAccessibility, string name)
			: base(syntax, containingNamespace, declaredAccessibility, name)
		{
		}

		public override IEnumerable<Declaration> GetMembers()
		{
			throw new System.NotImplementedException();
		}

		public override IEnumerable<Declaration> GetMembers(string name)
		{
			throw new System.NotImplementedException();
		}
	}
}
