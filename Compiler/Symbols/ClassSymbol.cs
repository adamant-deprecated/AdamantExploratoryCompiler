namespace Adamant.Exploratory.Compiler.Symbols
{
	public class ClassSymbol : DeclarationSymbol
	{
		public ClassSymbol(PackageSymbol containingPackage, NamespaceSymbol containingNamespace, string name)
			: base(containingPackage, containingNamespace, name)
		{
		}
	}
}
