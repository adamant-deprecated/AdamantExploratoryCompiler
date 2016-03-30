using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Binders.SymbolReferences
{
	internal class EntityReference : SymbolReference
	{
		public readonly Entity Entity;
		public override string Name => Entity.Name;

		public EntityReference(Entity entity)
		{
			Requires.NotNull(entity, nameof(entity));

			Entity = entity;
		}

		public override bool IsIn(PackageSyntax package)
		{
			return Entity.ContainingPackage == package;
		}

		public override bool IsVisibleFrom(PackageSyntax package)
		{
			return Entity.DeclaredAccessibility == Accessibility.Public
				|| (IsIn(package) && Entity.DeclaredAccessibility == Accessibility.Package);
		}

		public override IEnumerable<SymbolReference> GetMembers(string name)
		{
			return Entity.GetMembers(name).OfType<Entity>().Select(sym => new EntityReference(sym));
		}

		public static implicit operator Entity(EntityReference reference)
		{
			return reference.Entity;
		}

		public static implicit operator EntityReference(Entity entity)
		{
			return new EntityReference(entity);
		}
	}
}
