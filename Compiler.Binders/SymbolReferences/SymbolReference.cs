using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Binders.SymbolReferences
{
	/// <summary>
	/// A symbol reference referes to a symbol across a compilation
	/// </summary>
	public abstract class SymbolReference
	{
		public abstract string Name { get; }
		public abstract bool IsIn(Package package);
		public abstract bool IsVisibleFrom(Package package);
		public abstract IEnumerable<SymbolReference> GetMembers(string name);

		public static implicit operator SymbolReference(EntitySymbol entity)
		{
			return new EntityReference(entity);
		}
	}
}
