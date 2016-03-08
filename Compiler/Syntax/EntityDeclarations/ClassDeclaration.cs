using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Members;

namespace Adamant.Exploratory.Compiler.Syntax.EntityDeclarations
{
	public class ClassDeclaration : EntityDeclaration
	{
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
			var membersList = members.ToList();
			Constructors = membersList.OfType<Constructor>().ToList();
			Destructors = membersList.OfType<Destructor>().ToList();
			NamedMembers = new NamedMemberCollection(membersList.OfType<NamedMember>()
				.GroupBy(m => m.Name, (name1, members1) => members1
					.Aggregate((m1, m2) =>
					{
						var p1 = m1 as Property;
						var p2 = m2 as Property;
						if(p1 == null || p2 == null) throw new Exception($"Duplicate member definitions for '{name1}'");
						if(p1.Get != null && p2.Get != null) throw new Exception($"Duplicate getter declarations for '{name1}'");
						if(p1.Set != null && p2.Set != null) throw new Exception($"Duplicate setter declarations for '{name1}'");
						return new Property(name1, p1.Get ?? p2.Get, p1.Set ?? p2.Set);
					})));
		}

		public bool IsPartial { get; }
		public Safety Safety { get; }
		public bool IsSealed { get; }
		public bool IsAbstract { get; }

		public IReadOnlyList<Constructor> Constructors { get; }
		public IReadOnlyList<Destructor> Destructors { get; }
		public NamedMemberCollection NamedMembers { get; }
	}
}
