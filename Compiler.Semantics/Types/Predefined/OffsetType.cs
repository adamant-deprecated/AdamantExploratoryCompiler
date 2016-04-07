using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Predefined
{
	public class OffsetType : PredefinedType
	{
		public OffsetType(PredefinedTypeSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
			Requires.That(syntax.Name.Text == "offset", nameof(syntax), "Syntax must be for offset type");
		}
	}
}
