using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Types
{
	public interface ReferenceType : SemanticNode<ReferenceTypeSyntax>
	{
		new ReferenceTypeSyntax Syntax { get; }
		bool IsOwned { get; }
		bool IsMutable { get; }
		ValueType<ValueTypeSyntax> Type { get; }
	}
}