namespace Adamant.Exploratory.Compiler.Compiled
{
	public class CompiledDependency
	{
		public readonly CompiledPackage Package;
		public readonly string Alias;
		public readonly bool Trusted;

		public CompiledDependency(CompiledPackage package, string @alias, bool trusted)
		{
			Package = package;
			Alias = alias;
			Trusted = trusted;
		}
	}
}
