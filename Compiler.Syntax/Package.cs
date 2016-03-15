using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax.PackageConfig;

namespace Adamant.Exploratory.Compiler.Syntax
{
	/// <summary>
	/// A package is what a forge project produces.  The difference is that a project contains
	/// information about different configurations and targets as well as the build pipeline. A
	/// package is for a specific configuration, target etc.
	/// </summary>
	public class Package
	{
		public readonly string Name;
		public readonly IReadOnlyList<CompilationUnit> CompilationUnits;
		public readonly IReadOnlyList<PackageDependency> Dependencies;
		public readonly IReadOnlyList<Diagnostic> Diagnostics;
		// TODO Language version

		public Package(string name, IEnumerable<PackageDependency> dependencies)
			: this(name, new List<CompilationUnit>(), dependencies.ToList())
		{
		}

		private Package(string name, IReadOnlyList<CompilationUnit> compilationUnits, IReadOnlyList<PackageDependency> dependencies)
		{
			Requires.NotNullOrEmpty(name, nameof(name));
			Requires.That(dependencies.Select(d => d.AliasName).Distinct().Count() == dependencies.Count, nameof(dependencies), "Dependency names/alias must be unique");

			Name = name;
			CompilationUnits = compilationUnits;
			Dependencies = dependencies;
			var diagnostics = new DiagnosticsBuilder();
			foreach(var compilationUnit in CompilationUnits)
				diagnostics.Add(compilationUnit.Diagnostics);

			Diagnostics = diagnostics.Complete();
		}

		[Pure]
		public Package With(IEnumerable<CompilationUnit> compilationUnits)
		{
			return new Package(Name, compilationUnits.ToList(), Dependencies);
		}
	}
}
