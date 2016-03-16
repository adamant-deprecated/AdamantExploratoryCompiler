using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols.Namespaces;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols
{
	/// <summary>
	/// A package as a whole, forms the root of symbols
	/// </summary>
	public class PackageSymbol : Symbol
	{
		public readonly Package Package;
		public readonly PackageNamespaceSymbol GlobalNamespace;

		public PackageSymbol(Package package)
			: base(null, Accessibility.NotApplicable, package?.Name)
		{
			Requires.NotNull(package, nameof(package));

			Package = package;
			GlobalNamespace = new PackageNamespaceSymbol(this);
		}
	}
}
