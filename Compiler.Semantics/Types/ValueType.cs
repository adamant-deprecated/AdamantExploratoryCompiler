using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Types
{
	public interface ValueType<TSyntax> : SemanticNode<TSyntax>
		where TSyntax : ValueTypeSyntax
	{
		new ValueTypeSyntax Syntax { get; }
	}
}