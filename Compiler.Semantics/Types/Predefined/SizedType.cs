using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Predefined
{
	/// <summary>
	/// A sized type is a predefined type that has a length in bits
	/// </summary>
	public abstract class SizedType : PredefinedType
	{
		public int BitLength { get; }

		protected SizedType(PredefinedTypeSyntax syntax, Package containingPackage, int bitLength)
			: base(syntax, containingPackage)
		{
			BitLength = bitLength;
		}
	}
}
