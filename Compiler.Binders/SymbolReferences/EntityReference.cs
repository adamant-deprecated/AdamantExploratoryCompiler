using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Binders.SymbolReferences
{
	internal class EntityReference : SymbolReference
	{
		public readonly EntitySymbol Entity;
		public override string Name => Entity.Name;

		public EntityReference(EntitySymbol entity)
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

		public override IEnumerable<SymbolReference> GetMembers(string name)
		{
			return Entity.GetMembers(name).OfType<EntitySymbol>().Select(sym => new EntityReference(sym));
		}

		public static implicit operator EntitySymbol(EntityReference reference)
		{
			return reference.Entity;
		}

		public static implicit operator EntityReference(EntitySymbol entity)
		{
			return new EntityReference(entity);
		}
	}
}
