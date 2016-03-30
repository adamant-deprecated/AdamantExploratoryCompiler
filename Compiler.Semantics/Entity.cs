using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public interface Entity<out TSyntax> : Declaration<TSyntax>
			where TSyntax : EntitySyntax
	{
	}
}