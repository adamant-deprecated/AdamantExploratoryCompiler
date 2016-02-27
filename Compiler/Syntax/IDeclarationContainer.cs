using System.Collections.Generic;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public interface IDeclarationContainer
	{
		IReadOnlyList<Declaration> Declarations { get; }
	}
}
