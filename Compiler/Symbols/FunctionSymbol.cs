namespace Adamant.Exploratory.Compiler.Symbols
{
	public class FunctionSymbol : DeclarationSymbol
	{
		public FunctionSymbol(PackageSymbol containingPackage, NamespaceSymbol containingNamespace, string name)
			: base(containingPackage, containingNamespace, name)
		{
		}
	}
}
