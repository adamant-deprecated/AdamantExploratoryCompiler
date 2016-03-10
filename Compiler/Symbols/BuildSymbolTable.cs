using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public static class BuildSymbolTableExtensions
	{
		public static PackageSymbol BuildSymbolTable(this Package package)
		{
			return new PackageSymbol(package);
		}
	}
}
