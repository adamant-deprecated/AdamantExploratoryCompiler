using System;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Property : NamedMember
	{
		public static Symbol GetName = new Symbol("get");
		public static Symbol SetName = new Symbol("set");

		public Property(Symbol name, Method get, Method set)
			: base(name)
		{
			if(get == null && set == null) throw new ArgumentException($"Either '{nameof(get)}' or '{nameof(set)}' must be non-null");
			if(get != null && get.Name != GetName) throw new ArgumentException($"Getter name must be '{GetName}'", nameof(get));
			if(set != null && set.Name != SetName) throw new ArgumentException($"Getter name must be '{SetName}'", nameof(set));

			Get = get;
			Set = set;
		}

		public Method Get { get; private set; }
		public Method Set { get; private set; }
	}
}
