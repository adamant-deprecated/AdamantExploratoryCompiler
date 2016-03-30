using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Declarations
{
	public class FunctionDeclaration : Declaration
	{
		public readonly IReadOnlyList<FunctionSyntax> Syntax;

		public FunctionDeclaration(string name, IEnumerable<FunctionSyntax> syntax)
			: base(name)
		{
			Syntax = syntax.ToList();
		}
	}
}
