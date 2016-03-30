using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	/// <summary>
	/// A SemanticModel describes the semantics of a program element.  Currently, all semantic models
	/// correspond to some SyntaxNode(s).  However, in the future when IL is implemented, a semantic
	/// model might correspond to the IL loaded for a dependency.
	/// </summary>
	internal abstract class SemanticModel<TSyntax> : SemanticNode<TSyntax>
		where TSyntax : SyntaxNode
	{
		//private readonly List<Location> locations = new List<Location>();

		public IReadOnlyList<TSyntax> Syntax { get; }
		public abstract PackageModel ContainingPackage { get; }
		Package SemanticNode<TSyntax>.ContainingPackage => ContainingPackage;
		public bool IsPoisoned { get; private set; }
		//public IReadOnlyList<Location> Locations => locations;

		protected SemanticModel(TSyntax syntax)
		{
			// Right now, syntax won't be null, but in the future when IL is implemented it will be
			Requires.NotNull(syntax, nameof(syntax));

			Syntax = new List<TSyntax>(1) { syntax };
		}

		protected SemanticModel(IEnumerable<TSyntax> syntax)
		{
			Syntax = syntax.ToList();
		}

		public void Poison()
		{
			IsPoisoned = true;
		}
	}
}
