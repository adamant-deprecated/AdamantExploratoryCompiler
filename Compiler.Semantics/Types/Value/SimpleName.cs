using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Value
{
	public class SimpleName : Name
	{
		public new SimpleNameSyntax Syntax => (SimpleNameSyntax)base.Syntax;

		public SimpleName(SimpleNameSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
