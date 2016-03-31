using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	internal class GlobalVariableModel : EntityModel<GlobalVariableSyntax>
	{
		public GlobalVariableModel(GlobalVariableSyntax syntax, NamespaceModel containingNamespace)
			: base(syntax, containingNamespace, syntax.Accessibility, syntax.Name.ValueText)
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
