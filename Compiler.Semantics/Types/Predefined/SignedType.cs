using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Predefined
{
	/// <summary>
	/// A SignedType is a SizedType that can be signed or unsigned
	/// </summary>
	public abstract class SignedType : SizedType
	{
		public readonly bool IsSigned;

		protected SignedType(PredefinedTypeSyntax syntax, Package containingPackage, int bitLength, bool isSigned)
			: base(syntax, containingPackage, bitLength)
		{
			IsSigned = isSigned;
		}
	}
}
