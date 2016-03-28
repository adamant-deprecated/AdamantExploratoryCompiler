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
		public readonly Package Syntax;
		public readonly PackageSymbol Symbol;
		public readonly IReadOnlyList<Diagnostic> Diagnostics;

		public CompiledPackage(Package syntax, PackageSymbol symbol, IEnumerable<Diagnostic> diagnostics)
		{
			Requires.NotNull(syntax, nameof(syntax));
			Requires.NotNull(symbol, nameof(symbol));

			Name = syntax.Name;
			Syntax = syntax;
			Symbol = symbol;
			Diagnostics = diagnostics.ToList();
		}

		// TODO dependencies
	}
}
