using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public abstract class NameScope
	{
		public abstract Definition LookupInScope(Symbol name);
	}
}
