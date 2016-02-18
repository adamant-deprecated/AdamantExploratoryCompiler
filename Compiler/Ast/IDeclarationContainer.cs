using System.Collections.Generic;

namespace Adamant.Exploratory.Compiler.Ast
{
	public interface IDeclarationContainer
	{
		IEnumerable<Declaration> Declarations { get; } 
	}
}
