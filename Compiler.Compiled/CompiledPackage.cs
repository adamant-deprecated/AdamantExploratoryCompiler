using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Compiled
{
	public class CompiledPackage
	{
		public string Name;
		public readonly PackageSyntax Syntax;
		public readonly Package Symbol;
		public readonly IReadOnlyList<Function> EntryPoints;
		public readonly IReadOnlyList<CompiledDependency> Dependencies;
		public readonly IReadOnlyList<Diagnostic> Diagnostics;

		public CompiledPackage(PackageSyntax syntax, Package symbol, IEnumerable<Function> entryPoints, IEnumerable<Diagnostic> diagnostics, IEnumerable<CompiledDependency> dependencies)
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
