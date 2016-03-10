using System.Linq;
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

		public PackageSymbol(Package package)
			: base(package?.Name, Enumerable.Empty<Location>())
		{
			Requires.NotNull(package, nameof(package));

			Package = package;
		}
	}
}
