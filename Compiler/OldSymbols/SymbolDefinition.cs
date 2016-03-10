//using System;
//using Adamant.Exploratory.Compiler.Syntax.Modifiers;

//namespace Adamant.Exploratory.Compiler.OldSymbols
//{
//	public class SymbolDefinition
//	{
//		public readonly Definition Definition;
//		public readonly bool InCurrentPackage;

//		public SymbolDefinition(Definition definition, bool inCurrentPackage)
//		{
//			if(definition == null) throw new ArgumentNullException(nameof(definition));
//			Definition = definition;
//			InCurrentPackage = inCurrentPackage;
//		}

//		public bool IsVisible => Definition.Access == AccessModifier.Public
//								 || (InCurrentPackage && Definition.Access == AccessModifier.Package);
//	}
//}
