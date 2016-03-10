using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Symbols
{
	/// <summary>
	/// A Symbol represents some named entity in code
	/// </summary>
	public class Symbol
	{
		public readonly string Name;
		public readonly IReadOnlyList<Location> Locations;

		public Symbol(string name, IEnumerable<Location> locations)
		{
			Requires.NotNullOrEmpty(name, nameof(name));

			Name = name;
			Locations = locations.ToList();
		}
	}
}
