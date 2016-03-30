using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public interface Symbol<out TSyntax> : SemanticNode<TSyntax>
		where TSyntax : SyntaxNode
	{
		Accessibility DeclaredAccessibility { get; }
		string Name { get; }
	}
}