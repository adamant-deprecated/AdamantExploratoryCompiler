using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Predefined
{
	/// <summary>
	/// The Fixed type is not actually supported by this version of the compiler.  This class is just hear to indicate
	/// how it would fit into the semantic model
	/// </summary>
	public class FixedType : SignedType
	{
		public int FractionalBits { get; }

		public FixedType(PredefinedTypeSyntax syntax, Package containingPackage, int bitLength, bool isSigned, int fractionalBits)
			: base(syntax, containingPackage, bitLength, isSigned)
		{
			Requires.That(fractionalBits <= bitLength, nameof(fractionalBits), "Must be less than bit length");

			FractionalBits = fractionalBits;
		}
	}
}
