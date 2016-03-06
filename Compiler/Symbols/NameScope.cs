using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public abstract class NameScope
	{
		public abstract GlobalScope Globals { get; }
		public abstract Definition LookupLocal(Symbol name);
		public abstract SymbolDefinitions Lookup(Symbol name);

		public SymbolDefinitions Lookup(FullyQualifiedName name)
		{
			SymbolDefinitions defs = null;
			foreach(var symbol in name.Parts())
				defs = defs == null ? Lookup(symbol) : defs.Lookup(symbol);
			return defs;
		}
	}
}
