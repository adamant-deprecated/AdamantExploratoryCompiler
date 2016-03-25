using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class ConstructorSyntax : ClassMemberSyntax
	{
		public readonly Accessibility Access;
		public readonly Token Name;
		public readonly IReadOnlyList<ParameterSyntax> Parameters;
		public readonly IReadOnlyList<StatementSyntax> Body;

		public ConstructorSyntax(Accessibility access, Token name, IEnumerable<ParameterSyntax> parameters, IEnumerable<StatementSyntax> body)
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
