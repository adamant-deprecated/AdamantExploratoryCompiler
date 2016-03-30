using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public interface Declaration<out TSyntax> : Symbol<TSyntax>
		where TSyntax : DeclarationSyntax
	{
		Namespace ContainingNamespace { get; }
		IEnumerable<Declaration<DeclarationSyntax>> GetMembers();
		IEnumerable<Declaration<DeclarationSyntax>> GetMembers(string name);
	}
}