using System.Collections.Generic;

namespace Adamant.Exploratory.Compiler.Ast
{
	public interface IDeclarationContainer
	{
		IReadOnlyList<Declaration> Declarations { get; }
	}
}
