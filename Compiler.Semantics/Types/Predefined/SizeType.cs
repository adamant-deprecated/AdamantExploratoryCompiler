using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Predefined
{
	public class SizeType : PredefinedType
	{
		public SizeType(PredefinedTypeSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
			Requires.That(syntax.Name.Text == "size", nameof(syntax), "Syntax must be for size type");
		}
	}
}
