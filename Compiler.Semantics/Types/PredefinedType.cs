using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types
{
	public abstract class PredefinedType : ValueType
	{
		public new PredefinedTypeSyntax Syntax => (PredefinedTypeSyntax)base.Syntax;

		protected PredefinedType(PredefinedTypeSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
