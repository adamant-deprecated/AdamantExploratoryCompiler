using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// A SemanticModel describes the semantics of a program element.  Currently, all semantic models
	/// correspond to some SyntaxNode(s).  However, in the future when IL is implemented, a semantic
	/// model might correspond to the IL loaded for a dependency.
	/// </summary>
	public abstract class SemanticNode
	{
		//private readonly List<Location> locations = new List<Location>();
		//public IReadOnlyList<Location> Locations => locations;

		public IEnumerable<SyntaxNode> Syntax => GetSyntax();
		public abstract Package ContainingPackage { get; }
		public bool IsPoisoned { get; private set; }

		protected abstract IEnumerable<SyntaxNode> GetSyntax();

		public void Poison()
		{
			IsPoisoned = true;
		}
	}
}
