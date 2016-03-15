using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Constructor : Member
	{
		public readonly Accessibility Access;
		public readonly Token Name;
		public readonly IReadOnlyList<Parameter> Parameters;
		public readonly IReadOnlyList<Statement> Body;

		public Constructor(Accessibility access, Token name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
		{
			Requires.EnumDefined(access, nameof(access));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Access = access;
			Name = name;
			Parameters = parameters.ToList();
			Body = body.ToList();
		}
	}
}
