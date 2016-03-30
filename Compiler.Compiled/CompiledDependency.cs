namespace Adamant.Exploratory.Compiler.Compiled
{
	public class CompiledDependency// : IPackageSymbolReference
	{
		public readonly CompiledPackage Package;
		//Package IPackageSymbolReference.Package => Package.Symbol;
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
