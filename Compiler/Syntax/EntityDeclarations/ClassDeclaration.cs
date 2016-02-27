using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.EntityDeclarations
{
	public class ClassDeclaration : EntityDeclaration
	{
		private readonly IList<Member> members;

		public ClassDeclaration(
			AccessModifier access,
			bool isPartial,
			Safety safety,
			bool isSealed,
			bool isAbstract,
			FullyQualifiedName @namespace,
			Symbol name,
			IEnumerable<Member> members)
			: base(access, @namespace, name)
		{

			IsPartial = isPartial;
			Safety = safety;
			IsSealed = isSealed;
			IsAbstract = isAbstract;
			this.members = members.ToList();
		}

		public bool IsPartial { get; }
		public Safety Safety { get; }
		public bool IsSealed { get; }
		public bool IsAbstract { get; }

		public IEnumerable<Member> Members => members;
	}
}
