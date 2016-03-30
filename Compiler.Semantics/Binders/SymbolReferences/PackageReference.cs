using Adamant.Exploratory.Compiler.Semantics;

namespace Adamant.Exploratory.Compiler.Binders.SymbolReferences
{
	public interface IPackageSymbolReference
	{
		Package Package { get; }
		string Alias { get; }
		bool Trusted { get; }
	}
}
