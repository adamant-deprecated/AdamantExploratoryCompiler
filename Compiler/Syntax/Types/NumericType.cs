namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class NumericType : PlainType
	{
		public readonly string Kind;

		public NumericType(string typeName)
		{
			Kind = typeName;
			// TODO really parse the type name
		}
	}
}
