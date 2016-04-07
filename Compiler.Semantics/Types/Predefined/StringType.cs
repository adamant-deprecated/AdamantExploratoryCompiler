using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Predefined
{
	public class StringType : PredefinedType
	{
		public StringType(PredefinedTypeSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
			Requires.That(syntax.Name.Text == "string", nameof(syntax), "Syntax must be for size type");
		}
	}
}
