using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Adamant.Exploratory.Common;
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
		public readonly IReadOnlyDictionary<string, PackageDependency> Dependencies;
		// TODO Language version

		public Package(string name, IEnumerable<PackageDependency> dependencies)
			: this(name, new List<CompilationUnit>(), dependencies.ToDictionary(p => p.Name, p => p))
		{
		}

		private Package(string name, IReadOnlyList<CompilationUnit> compilationUnits, IReadOnlyDictionary<string, PackageDependency> dependencies)
		{
			Requires.NotNullOrEmpty(name, nameof(name));

			Name = name;
			CompilationUnits = compilationUnits;
			Dependencies = dependencies;
		}

		[Pure]
		public Package With(IEnumerable<CompilationUnit> compilationUnits)
		{
			return new Package(Name, compilationUnits.ToList(), Dependencies);
		}
	}
}
