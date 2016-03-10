using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Method : NamedMember
	{
		public readonly AccessModifier Access;
		public readonly IReadOnlyList<Parameter> Parameters;
		public readonly IReadOnlyList<Statement> Body;

		public Method(AccessModifier access, Token name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(name)
		{
			Requires.EnumDefined(access, nameof(access));

			Access = access;
			Parameters = parameters.ToList();
			Body = body.ToList();
		}
	}
}
