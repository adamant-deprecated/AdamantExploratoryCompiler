using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public abstract class Entity : Declaration
	{
		public new IEnumerable<EntitySyntax> Syntax => base.Syntax.Cast<EntitySyntax>();

		protected Entity(EntitySyntax syntax, Namespace containingNamespace, Accessibility declaredAccessibility, string name)
			: base(syntax, containingNamespace.ContainingPackage, containingNamespace, declaredAccessibility, name)
		{
			Requires.NotNull(containingNamespace, nameof(containingNamespace));
		}

		public override IEnumerable<Declaration> GetMembers()
		{
			return Enumerable.Empty<Declaration>();
		}

		public override IEnumerable<Declaration> GetMembers(string name)
		{
			return Enumerable.Empty<Declaration>();
		}
	}
}
