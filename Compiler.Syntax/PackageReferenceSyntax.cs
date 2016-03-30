using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class PackageReferenceSyntax : SyntaxNode
	{
		public readonly string Name;
		public readonly string Alias;
		public readonly bool Trusted;

		public PackageReferenceSyntax(string name, string alias, bool trusted)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
			Alias = alias;
			Trusted = trusted;
		}

		public string AliasName => Alias ?? Name;
	}
}
