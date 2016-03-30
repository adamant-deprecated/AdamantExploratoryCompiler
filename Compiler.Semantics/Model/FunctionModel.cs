using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	internal class FunctionModel : EntityModel<FunctionSyntax>, Function
	{
		// TODO return type

		public FunctionModel(FunctionSyntax syntax, NamespaceModel containingNamespace, Accessibility declaredAccessibility, string name)
			: base(syntax, containingNamespace, declaredAccessibility, name)
		{
		}

		public override IEnumerable<Declaration<DeclarationSyntax>> GetMembers()
		{
			return Enumerable.Empty<Declaration<DeclarationSyntax>>();
		}

		public override IEnumerable<Declaration<DeclarationSyntax>> GetMembers(string name)
		{
			return Enumerable.Empty<Declaration<DeclarationSyntax>>();
		}
	}
}
