using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	/// <summary>
	/// A package as a whole, forms the root of symbols
	/// </summary>
	public class PackageSymbol : Symbol
	{
		public readonly Package Package;
		public readonly NamespaceSymbol GlobalNamespace;

		public PackageSymbol(Package package)
			: base(package?.Name)
		{
			Requires.NotNull(package, nameof(package));

			Package = package;
			GlobalNamespace = new NamespaceSymbol(this);
		}

		public override PackageSymbol ContainingPackage => this;
	}
}
