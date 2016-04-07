using Adamant.Exploratory.Compiler.Semantics.Types;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public interface Function : Entity<FunctionSyntax>
	{
		new FunctionSyntax Syntax { get; }
		ReferenceType ReturnType { get; }
	}
}