using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Types
{
	public abstract class ValueType : Expression
	{
		public new ValueTypeSyntax Syntax => (ValueTypeSyntax)base.Syntax;

		protected ValueType(ValueTypeSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
