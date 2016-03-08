using System;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class NamedMember : Member
	{
		protected NamedMember(Symbol name)
		{
			if(name == null) throw new ArgumentNullException(nameof(name));

			Name = name;
		}

		public Symbol Name { get; }
	}
}
