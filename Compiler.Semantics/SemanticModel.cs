using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// A SemanticModel describes the semantics of a program element.  Currently, all semantic models
	/// correspond to some SyntaxNode(s).  However, in the future when IL is implemented, a semantic
	/// model might correspond to the IL loaded for a dependency.
	/// </summary>
	public abstract class SemanticModel
	{
		private static readonly IReadOnlyList<SemanticModel> NoMembers = new List<SemanticModel>(0);

		private readonly List<Location> locations = new List<Location>();

		public PackageSyntax ContainingPackage { get; }
		public readonly Accessibility DeclaredAccessibility;
		public readonly string Name;
		public IReadOnlyList<Location> Locations => locations;

		protected SemanticModel(PackageSyntax containingPackage, Accessibility declaredAccessibility, string name)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
			ContainingPackage = containingPackage;
			DeclaredAccessibility = declaredAccessibility;
		}

		public bool IsPoisoned { get; private set; }

		public void Poison()
		{
			IsPoisoned = true;
		}

		// This pattern allows us to simulate covarient return types
		protected virtual IReadOnlyList<SemanticModel> GetMembersInternal(string name)
		{
			return NoMembers;
		}

		public IReadOnlyList<SemanticModel> GetMembers(string name)
		{
			return GetMembersInternal(name);
		}
	}
}
