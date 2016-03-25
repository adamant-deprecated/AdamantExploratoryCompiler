using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class MethodSyntax : NamedClassMemberSyntax
	{
		public readonly Accessibility Access;
		public readonly IReadOnlyList<ParameterSyntax> Parameters;
		public readonly IReadOnlyList<StatementSyntax> Body;

		public MethodSyntax(Accessibility access, Token name, IEnumerable<ParameterSyntax> parameters, IEnumerable<StatementSyntax> body)
			: base(name)
		{
			Requires.EnumDefined(access, nameof(access));

			Access = access;
			Parameters = parameters.ToList();
			Body = body.ToList();
		}
	}
}
