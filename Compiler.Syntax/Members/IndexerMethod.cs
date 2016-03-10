using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class IndexerMethod : Member
	{
		public readonly AccessModifier Access;
		public readonly AccessorType Type;
		public readonly IReadOnlyList<Parameter> Parameters;
		public readonly IReadOnlyList<Statement> Body;

		public IndexerMethod(AccessModifier access, AccessorType type, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
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
