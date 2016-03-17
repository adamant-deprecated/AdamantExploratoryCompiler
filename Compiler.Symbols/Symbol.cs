using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols
{
	/// <summary>
	/// A Symbol represents some named entity in code
	/// </summary>
	public abstract class Symbol
	{
		private static readonly IReadOnlyList<Symbol> NoMembers = new List<Symbol>(0);

		private readonly List<Location> locations = new List<Location>();

		public Package ContainingPackage { get; }
		public readonly Accessibility DeclaredAccessibility;
		public readonly string Name;
		public IReadOnlyList<Location> Locations => locations;

		protected Symbol(Package containingPackage, Accessibility declaredAccessibility, string name)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
			ContainingPackage = containingPackage;
			DeclaredAccessibility = declaredAccessibility;
		}

		// This pattern allows us to simulate covarient return types
		protected virtual IReadOnlyList<Symbol> GetMembersInternal(string name)
		{
			return NoMembers;
		}

		public IReadOnlyList<Symbol> GetMembers(string name)
		{
			return GetMembersInternal(name);
		}
	}
}
