using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public interface SemanticNode<out TSyntax>
		where TSyntax : SyntaxNode
	{
		IReadOnlyList<TSyntax> Syntax { get; }
		Package ContainingPackage { get; }
	}
}