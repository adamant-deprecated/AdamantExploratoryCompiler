using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Semantics.NamespaceMembers
{
	internal sealed class Entity : NamespaceMember
	{
		public readonly Semantics.Entity Symbol;

		public Entity(Semantics.Entity symbol)
		{
			Requires.NotNull(symbol, nameof(symbol));

			Symbol = symbol;
		}

		public static implicit operator Entity(Entity entity)
		{
			return entity.Symbol;
		}
	}
}