using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Declarations
{
	public class NamespaceDeclaration : Declaration
	{
		public readonly IReadOnlyList<NamespaceSyntax> Syntax;
		public readonly IReadOnlyList<Declaration> Members;

		public NamespaceDeclaration(string name, IEnumerable<NamespaceSyntax> syntax, IEnumerable<Declaration> members)
			: base(name)
		{
			Syntax = syntax.ToList();
			Members = members.ToList();
		}
	}
}