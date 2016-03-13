using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class PackageBinder : Binder
	{
		private readonly PackageSymbol package;
		public readonly ContainerBinder GlobalNamespace;

		public PackageBinder(PackageSymbol package)
		{
			Requires.NotNull(package, nameof(package));

			this.package = package;
			GlobalNamespace = new ContainerBinder(package.GlobalNamespace);
		}
	}
}
