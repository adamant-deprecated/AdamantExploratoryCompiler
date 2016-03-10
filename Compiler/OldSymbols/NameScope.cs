//namespace Adamant.Exploratory.Compiler.OldSymbols
//{
//	public abstract class NameScope
//	{
//		public abstract GlobalScope Globals { get; }

//		public abstract SymbolDefinitions Lookup(Symbol name, DefinitionKind kind = DefinitionKind.Any);
//		public SymbolDefinitions Lookup(FullyQualifiedName name, DefinitionKind kind = DefinitionKind.Any)
//		{
//			SymbolDefinitions defs = null;
//			foreach(var symbol in name.Parts())
//				defs = defs == null ? Lookup(symbol, kind) : defs.Lookup(symbol);
//			return defs;
//		}

//		public abstract Definition LookupInCurrentScopeOnly(Symbol name, DefinitionKind kind = DefinitionKind.Any);
//	}
//}
