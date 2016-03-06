using System;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class SymbolDefinition
	{
		public readonly Definition Definition;
		public readonly bool InCurrentPackage;

		public SymbolDefinition(Definition definition, bool inCurrentPackage)
		{
			if(definition == null) throw new ArgumentNullException(nameof(definition));
			Definition = definition;
			InCurrentPackage = inCurrentPackage;
		}

		public bool IsVisible => Definition.Access == AccessModifier.Public
								 || (InCurrentPackage && Definition.Access == AccessModifier.Package);
	}
}
