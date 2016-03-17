using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Binders.SymbolReferences
{
	public interface IPackageSymbolReference
	{
		PackageSymbol PackageSymbol { get; }
		string Alias { get; }
		bool Trusted { get; }
	}
}
