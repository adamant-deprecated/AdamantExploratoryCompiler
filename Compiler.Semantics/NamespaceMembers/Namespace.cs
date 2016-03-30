using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Symbols.NamespaceMembers
{
	internal sealed class Namespace : NamespaceMember
	{
		public readonly string Name;
		public readonly IReadOnlyList<NamespaceMember> Member;

		public Namespace(string name, IEnumerable<NamespaceMember> members)
		{
			Name = name;
			Member = members.ToList();
		}
	}
}