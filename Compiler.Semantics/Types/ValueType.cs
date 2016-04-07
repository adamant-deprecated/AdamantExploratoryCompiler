using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Types
{
	public abstract class ValueType : SourceSemanticNode
	{
		public new ValueTypeSyntax Syntax => (ValueTypeSyntax)base.Syntax;

		public ValueType(ValueTypeSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
