using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	/// <summary>
	/// A convenience base class so models don't have to duplicate handling of ContainingPackage
	/// and singular syntax
	/// </summary>
	internal class SemanticElementModel<TSyntax> : SemanticNodeModel<TSyntax>
		where TSyntax : SyntaxNode
	{
		public new TSyntax Syntax => base.Syntax.SingleOrDefault();
		public override PackageModel ContainingPackage { get; }

		public SemanticElementModel(TSyntax syntax, PackageModel containingPackage)
			: base(syntax)
		{
			Requires.NotNull(containingPackage, nameof(containingPackage));

			ContainingPackage = containingPackage;
		}
	}
}
