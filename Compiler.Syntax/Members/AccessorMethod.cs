using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class AccessorMethod : NamedMember
	{
		public readonly Accessibility Access;
		public readonly AccessorType Type;
		public readonly IReadOnlyList<Parameter> Parameters;
		public readonly IReadOnlyList<Statement> Body;

		public AccessorMethod(Accessibility access, AccessorType type, Token name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
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
