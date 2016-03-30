using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	internal class ClassModel : EntityModel<ClassSyntax>, Class
	{
		public ClassModel(ClassSyntax syntax, NamespaceModel containingNamespace, Accessibility declaredAccessibility, string name)
			: base(syntax, containingNamespace, declaredAccessibility, name)
		{
		}

		public override IEnumerable<Declaration<DeclarationSyntax>> GetMembers()
		{
			throw new System.NotImplementedException();
		}

		public override IEnumerable<Declaration<DeclarationSyntax>> GetMembers(string name)
		{
			throw new System.NotImplementedException();
		}
	}
}
