using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Types
{
	public class ReferenceType : SourceSemanticNode
	{
		public new ReferenceTypeSyntax Syntax => (ReferenceTypeSyntax)base.Syntax;
		public bool IsOwned { get; }
		public bool IsMutable => Syntax.IsMutable;
		public ValueType Type { get;  }

		public ReferenceType(ReferenceTypeSyntax syntax, Package containingPackage, ValueType type)
			: base(syntax, containingPackage)
		{
			IsOwned = syntax.IsOwned ?? false; // TODO actually infer ownership
			Type = type;
		}
	}
}
