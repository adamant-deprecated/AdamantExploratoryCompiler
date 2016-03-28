using Adamant.Exploratory.Compiler.Binders.SymbolReferences;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Compiled
{
	public class CompiledDependency : IPackageSymbolReference
	{
		public readonly CompiledPackage Package;
		PackageSymbol IPackageSymbolReference.PackageSymbol => Package.Symbol;
		public string Alias { get; }
		public bool Trusted { get; }

		public CompiledDependency(CompiledPackage package, string @alias, bool trusted)
		{
			Package = package;
			Alias = alias;
			Trusted = trusted;
		}
	}
}
