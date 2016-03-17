﻿using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class ClassSymbol : EntitySymbol
	{
		public ClassSymbol(Package containingPackage,  Accessibility declaredAccessibility, string name)
			: base(containingPackage, declaredAccessibility, name)
		{
		}
	}
}