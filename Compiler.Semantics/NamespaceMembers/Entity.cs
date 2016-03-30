using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Symbols.NamespaceMembers
{
	internal sealed class Entity : NamespaceMember
	{
		public readonly EntitySymbol Symbol;

		public Entity(EntitySymbol symbol)
		{
			Requires.NotNull(symbol, nameof(symbol));

			Symbol = symbol;
		}

		public static implicit operator EntitySymbol(Entity entity)
		{
			return entity.Symbol;
		}
	}
}