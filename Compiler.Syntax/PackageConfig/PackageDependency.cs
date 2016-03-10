using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.PackageConfig
{
	public class PackageDependency
	{
		public readonly string Name;
		public readonly string Alias;
		public readonly bool Trusted;

		public PackageDependency(string name, string alias, bool trusted)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
			Alias = alias;
			Trusted = trusted;
		}

		public string AliasName => Alias ?? Name;
	}
}
