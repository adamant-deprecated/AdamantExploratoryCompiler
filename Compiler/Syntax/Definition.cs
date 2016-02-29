using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public interface Definition
	{
		FullyQualifiedName Namespace { get; }
		Symbol Name { get; }
		FullyQualifiedName FullyQualifiedName { get; }
	}
}
