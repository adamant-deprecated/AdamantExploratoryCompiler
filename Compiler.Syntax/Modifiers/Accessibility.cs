using System;

namespace Adamant.Exploratory.Compiler.Syntax.Modifiers
{
	public enum Accessibility
	{
		NotApplicable = 0,
		Private,
		Protected,
		Package,
		Public,
	}

	public static class AccessibilityExtensions
	{
		public static Accessibility MostVisible(this Accessibility x, Accessibility y)
		{
			return (Accessibility)Math.Max((int)x, (int)y);
		}
	}
}
