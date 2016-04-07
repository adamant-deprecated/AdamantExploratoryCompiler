using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Predefined
{
	public class IntType : SignedType
	{
		public IntType(PredefinedTypeSyntax syntax, Package containingPackage, int bitLength, bool isSigned)
			: base(syntax, containingPackage, bitLength, isSigned)
		{
			var prefix = isSigned ? "int" : "uint";
			Requires.That(syntax.Name.Text.StartsWith(prefix), nameof(syntax), $"Syntax must be for {prefix} type");
		}
	}
}
