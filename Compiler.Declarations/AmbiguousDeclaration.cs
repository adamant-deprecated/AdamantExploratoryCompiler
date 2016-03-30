using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Declarations
{
	public class AmbiguousDeclaration : Declaration
	{
		public readonly IReadOnlyList<Declaration> Declarations;

		public AmbiguousDeclaration(string name, IEnumerable<Declaration> declarations)
			: base(name)
		{
			Declarations = declarations.ToList();
		}
	}
}
