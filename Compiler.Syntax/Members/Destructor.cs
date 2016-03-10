using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Destructor : Member
	{
		public readonly AccessModifier Access;
		public readonly IReadOnlyList<Parameter> Parameters;
		public readonly IReadOnlyList<Statement> Body;

		public Destructor(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
		{
			Requires.EnumDefined(access, nameof(access));

			Access = access;
			Parameters = parameters.ToList();
			Body = body.ToList();
		}
	}
}
