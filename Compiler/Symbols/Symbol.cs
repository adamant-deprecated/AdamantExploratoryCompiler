using System.Collections.Generic;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Symbols
{
	/// <summary>
	/// A Symbol represents some named entity in code
	/// </summary>
	public abstract class Symbol
	{
		public readonly string Name;
		private readonly List<Location> locations = new List<Location>();

		public Symbol(string name)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
		}

		public abstract PackageSymbol ContainingPackage { get; }
		public IReadOnlyList<Location> Locations => locations;
	}
}
