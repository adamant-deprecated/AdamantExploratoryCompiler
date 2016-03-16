using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public abstract class Name : ValueType
	{
		public abstract TextPosition Position { get; }
	}
}
