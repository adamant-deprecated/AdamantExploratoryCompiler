using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public abstract class NameSyntax : ValueTypeSyntax
	{
		public abstract TextPosition Position { get; }
	}
}
