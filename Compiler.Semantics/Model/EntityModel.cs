using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	internal abstract class EntityModel<TSyntax> : DeclarationModel<TSyntax>, Entity<TSyntax>
		where TSyntax : EntitySyntax
	{
		protected EntityModel(TSyntax syntax, NamespaceModel containingNamespace, Accessibility declaredAccessibility, string name)
			: base(syntax, containingNamespace.ContainingPackage, containingNamespace, declaredAccessibility, name)
		{
			Requires.NotNull(containingNamespace, nameof(containingNamespace));
		}
	}
}
