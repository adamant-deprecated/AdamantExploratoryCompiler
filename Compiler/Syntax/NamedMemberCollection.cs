using System.Collections.Generic;
using System.Collections.ObjectModel;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class NamedMemberCollection : KeyedCollection<Symbol, NamedMember>
	{
		public NamedMemberCollection()
		{
		}

		public NamedMemberCollection(IEnumerable<NamedMember> members)
		{
			foreach(var member in members)
				Add(member);
		}

		protected override Symbol GetKeyForItem(NamedMember member)
		{
			return member.Name;
		}

		public bool TryGetValue(Symbol symbol, out NamedMember member)
		{
			if(Dictionary != null) return Dictionary.TryGetValue(symbol, out member);
			member = default(NamedMember);
			return false;
		}

		public new bool Contains(NamedMember member)
		{
			return Contains(GetKeyForItem(member));
		}

		public NamedMember TryGetValue(Symbol symbol)
		{
			NamedMember member;
			// If no keys have been added, there will be no dictionary
			return Dictionary == null || !Dictionary.TryGetValue(symbol, out member) ? null : member;
		}
	}
}
