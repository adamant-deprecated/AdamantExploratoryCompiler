using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class ClassDeclaration : EntityDeclaration
	{
		public readonly bool IsPartial;
		public readonly Safety Safety;
		public readonly bool IsSealed;
		public readonly bool IsAbstract;
		public readonly IReadOnlyList<Member> Members;

		public ClassDeclaration(
			Accessibility accessibility, 
			bool isPartial, 
			Safety safety, 
			bool isSealed,
			bool isAbstract,
			Token name,
			IEnumerable<Member> members)
			: base(accessibility, name)
		{
			Requires.EnumDefined(safety, nameof(safety));

			IsPartial = isPartial;
			Safety = safety;
			IsSealed = isSealed;
			IsAbstract = isAbstract;
			Members = members.ToList();
		}
	}
}
