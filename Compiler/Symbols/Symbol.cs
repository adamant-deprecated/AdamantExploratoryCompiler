using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols
{
	/// <summary>
	/// A Symbol represents some named entity in code
	/// </summary>
	public abstract class Symbol
	{
		private readonly List<Location> locations = new List<Location>();

		public readonly PackageSymbol ContainingPackage;
		public readonly Accessibility DeclaredAccessibility;
		public readonly string Name;
		public IReadOnlyList<Location> Locations => locations;

		protected Symbol(PackageSymbol containingPackage, Accessibility declaredAccessibility, string name)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
			ContainingPackage = containingPackage;
			DeclaredAccessibility = declaredAccessibility;
		}
	}
}
