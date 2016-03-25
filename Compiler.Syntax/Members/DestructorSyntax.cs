using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class DestructorSyntax : ClassMemberSyntax
	{
		public readonly Accessibility Access;
		public readonly IReadOnlyList<ParameterSyntax> Parameters;
		public readonly IReadOnlyList<StatementSyntax> Body;

		public DestructorSyntax(Accessibility access, IEnumerable<ParameterSyntax> parameters, IEnumerable<StatementSyntax> body)
		{
			Requires.EnumDefined(access, nameof(access));

			Access = access;
			Parameters = parameters.ToList();
			Body = body.ToList();
		}
	}
}
