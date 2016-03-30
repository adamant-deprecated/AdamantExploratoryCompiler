using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Compiled
{
	public class CompiledPackage
	{
		public string Name;
		public readonly PackageSyntax Syntax;
		public readonly PackageSymbol Symbol;
		public readonly IReadOnlyList<FunctionSymbol> EntryPoints;
		public readonly IReadOnlyList<CompiledDependency> Dependencies;
		public readonly IReadOnlyList<Diagnostic> Diagnostics;

		public CompiledPackage(PackageSyntax syntax, PackageSymbol symbol, IEnumerable<FunctionSymbol> entryPoints, IEnumerable<Diagnostic> diagnostics, IEnumerable<CompiledDependency> dependencies)
		{
			Requires.NotNull(syntax, nameof(syntax));
			Requires.NotNull(symbol, nameof(symbol));

			Name = syntax.Name;
			Syntax = syntax;
			Symbol = symbol;
			EntryPoints = entryPoints.ToList();
			Dependencies = dependencies.ToList();
			Diagnostics = diagnostics.ToList();
		}
	}
}
