namespace Adamant.Exploratory.Compiler.Semantics.Binders.SymbolReferences
{
	public interface IPackageSymbolReference
	{
		Package Package { get; }
		string Alias { get; }
		bool Trusted { get; }
	}
}
