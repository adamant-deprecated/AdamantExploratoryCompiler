using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Compiled;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Binders
{
	public class PackageBinder : Binder
	{
		public readonly PackageSymbol Symbol;
		public readonly ContainerBinder GlobalNamespace;
		private readonly List<CompiledDependency> dependencies;

		public PackageBinder(PackageSymbol symbol, IEnumerable<CompiledDependency> dependencies)
		{
			Requires.NotNull(symbol, nameof(symbol));

			Symbol = symbol;
			GlobalNamespace = new ContainerBinder(this, symbol.GlobalNamespace);
			this.dependencies = dependencies.ToList();
		}

		public IReadOnlyList<CompiledDependency> Dependencies => dependencies;
	}
}
