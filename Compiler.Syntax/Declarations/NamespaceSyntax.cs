using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Directives;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class NamespaceSyntax : DeclarationSyntax
	{
		public readonly IReadOnlyList<SyntaxToken> Names;
		public readonly IReadOnlyList<UsingSyntax> UsingDirectives;
		public readonly IReadOnlyList<DeclarationSyntax> Members;

		public NamespaceSyntax(IEnumerable<SyntaxToken> names, IEnumerable<UsingSyntax> usingDirectives, IEnumerable<DeclarationSyntax> declarations)
		{
			Names = names.ToList();
			Requires.That(Names.Count > 0, nameof(names), "Must be at least one name");
			foreach(var token in Names)
			{
				Requires.NotNull(token, "name");
				SyntaxRequires.TypeIs(token, SyntaxTokenType.Identifier, "name");
			}
			UsingDirectives = usingDirectives.ToList();
			Members = declarations.ToList();
		}
	}
}
