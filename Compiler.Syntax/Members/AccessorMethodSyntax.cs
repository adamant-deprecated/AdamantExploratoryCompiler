using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class AccessorMethodSyntax : NamedClassMemberSyntax
	{
		public readonly Accessibility Access;
		public readonly AccessorMethodType Type;
		public readonly IReadOnlyList<ParameterSyntax> Parameters;
		public readonly IReadOnlyList<StatementSyntax> Body;

		public AccessorMethodSyntax(Accessibility access, AccessorMethodType type, Token name, IEnumerable<ParameterSyntax> parameters, IEnumerable<StatementSyntax> body)
			: base(name)
		{
			Requires.EnumDefined(access, nameof(access));
			Requires.EnumDefined(type, nameof(type));

			Access = access;
			Type = type;
			Parameters = parameters.ToList();
			Body = body.ToList();
		}
	}
}
