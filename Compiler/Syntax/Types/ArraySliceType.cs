namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class ArraySliceType : PlainType
	{
		public ArraySliceType(Type elementType, int dimensions)
		{
			ElementType = elementType;
			Dimensions = dimensions;
		}

		public Type ElementType { get; }
		public int Dimensions { get; }
	}
}
