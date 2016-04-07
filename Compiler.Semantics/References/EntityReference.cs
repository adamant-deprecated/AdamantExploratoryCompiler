using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics.References
{
	internal class EntityReference : DeclarationReference
	{
		public readonly Entity Entity;
		public override string Name => Entity.Name;

		public EntityReference(Entity entity)
		{
			Requires.NotNull(entity, nameof(entity));

			Entity = entity;
		}

		public override bool IsIn(Package package)
		{
			return Entity.ContainingPackage == package;
		}

		public override bool IsVisibleFrom(Package package)
		{
			return Entity.DeclaredAccessibility == Accessibility.Public
				|| (IsIn(package) && Entity.DeclaredAccessibility == Accessibility.Package);
		}

		public override IEnumerable<DeclarationReference> GetMembers(string name)
		{
			return Entity.GetMembers(name).OfType<Entity>().Select(sym => new EntityReference(sym));
		}
	}
}
