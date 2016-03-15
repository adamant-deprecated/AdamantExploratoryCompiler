using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Compiled
{
	public class CompiledPackage
	{
		public string Name;
		public readonly Package Package;
		public readonly PackageSymbols Symbols;

		public CompiledPackage(Package package, PackageSymbols symbols)
		{
			Requires.NotNull(package, nameof(package));
			Requires.NotNull(symbols, nameof(symbols));

			Name = package.Name;
			Package = package;
			Symbols = symbols;
		}

		// TODO dependencies
	}
}
