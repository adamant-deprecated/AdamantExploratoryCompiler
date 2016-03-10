using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Directives;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class NamespaceDeclaration : Declaration
	{
		public readonly Name Name;
		public readonly IReadOnlyList<UsingDirective> UsingDirectives;
		public readonly IReadOnlyList<Declaration> Declarations;

		public NamespaceDeclaration(Name name, IEnumerable<UsingDirective> usingDirectives, IEnumerable<Declaration> declarations)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
			UsingDirectives = usingDirectives.ToList();
			Declarations = declarations.ToList();
		}
	}
}
