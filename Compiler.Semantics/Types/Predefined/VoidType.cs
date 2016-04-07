using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Predefined
{
	public class VoidType : PredefinedType
	{
		public VoidType(PredefinedTypeSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
			Requires.That(syntax.Name.Text == "void", nameof(syntax), "Syntax must be for void type");
		}
	}
}
