using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public interface Definition
	{
		AccessModifier Access { get; }
		FullyQualifiedName Namespace { get; }
		Symbol Name { get; }
		FullyQualifiedName FullyQualifiedName { get; }
		Definitions Definitions { get; }
	}
}
