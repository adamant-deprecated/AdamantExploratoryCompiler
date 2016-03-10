using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Directives;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class NamespaceDeclaration : Declaration
	{
		public readonly IReadOnlyList<Token> Names;
		public readonly IReadOnlyList<UsingDirective> UsingDirectives;
		public readonly IReadOnlyList<Declaration> Members;

		public NamespaceDeclaration(IEnumerable<Token> names, IEnumerable<UsingDirective> usingDirectives, IEnumerable<Declaration> declarations)
		{
			Names = names.ToList();
			Requires.That(Names.Count > 0, nameof(names), "Must be at least one name");
			foreach(var token in Names)
			{
				Requires.NotNull(token, "name");
				SyntaxRequires.TypeIs(token, TokenType.Identifier, "name");
			}
			UsingDirectives = usingDirectives.ToList();
			Members = declarations.ToList();
		}
	}
}
