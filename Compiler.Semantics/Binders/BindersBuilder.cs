using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	internal class BindersBuilder
	{
		private readonly Package package;

		public BindersBuilder(Package package)
		{
			this.package = package;
		}

		public IReadOnlyDictionary<SyntaxNode, Binder> Build(DiagnosticsBuilder diagnostics)
		{
			var binders = new Dictionary<SyntaxNode, Binder>();
			// The package binder doesn't go in the binders collection, it just serves as the root binder
			var packageBinder = new PackageBinder(package);
			foreach(var compilationUnit in package.Syntax.CompilationUnits)
				new CompilationUnitBindersBuilder(binders, package, compilationUnit, diagnostics).Build(packageBinder);

			return binders;
		}
	}
}
