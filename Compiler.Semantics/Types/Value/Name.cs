using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Value
{
	public class Name : ValueType
	{
		public new NameSyntax Syntax => (NameSyntax)base.Syntax;

		public Name(NameSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
