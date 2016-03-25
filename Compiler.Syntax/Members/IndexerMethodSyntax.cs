using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class IndexerMethodSyntax : ClassMemberSyntax
	{
		public readonly Accessibility Access;
		public readonly AccessorMethodType Type;
		public readonly IReadOnlyList<ParameterSyntax> Parameters;
		public readonly IReadOnlyList<StatementSyntax> Body;

		public IndexerMethodSyntax(Accessibility access, AccessorMethodType type, IEnumerable<ParameterSyntax> parameters, IEnumerable<StatementSyntax> body)
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
