using System;

namespace Adamant.Exploratory.Compiler.Syntax.Modifiers
{
	public enum AccessModifier
	{
		Private,
		Protected,
		Package,
		Public,
	}

	public static class AccessModifierExtensions
	{
		public static AccessModifier MostVisible(this AccessModifier x, AccessModifier y)
		{
			return (AccessModifier)Math.Max((int)x, (int)y);
		}
	}
}
