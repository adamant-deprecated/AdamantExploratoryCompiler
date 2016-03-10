namespace Adamant.Exploratory.Compiler.Symbols
{
	public abstract class DeclarationSymbol : Symbol
	{
		public readonly NamespaceSymbol ContainingNamespace;

		protected DeclarationSymbol(PackageSymbol containingPackage, NamespaceSymbol containingNamespace, string name)
			: base(name)
		{
			ContainingPackage = containingPackage;
			ContainingNamespace = containingNamespace;
		}

		public override PackageSymbol ContainingPackage { get; }
	}
}
