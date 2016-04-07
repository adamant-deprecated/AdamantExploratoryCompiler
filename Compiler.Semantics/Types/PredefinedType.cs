using System;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types
{
	public class PredefinedType : ValueType
	{
		public new PredefinedTypeSyntax Syntax => (PredefinedTypeSyntax)base.Syntax;
		public PredefinedTypeKind Kind { get; }
		public int? BitSize { get; }
		public int? FractionalLength { get; }
		public bool? Signed { get; }

		public PredefinedType(PredefinedTypeSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
			FractionalLength = null;
			switch(syntax.Name.ValueText)
			{
				case "byte":
					Kind = PredefinedTypeKind.Int;
					BitSize = 8;
					Signed = false;
					break;
				case "int":
					Kind = PredefinedTypeKind.Int;
					BitSize = 32;
					Signed = true;
					break;
				case "string":
					Kind = PredefinedTypeKind.String;
					BitSize = null;
					Signed = null;
					break;
				case "UnsafeArray":
					Kind = PredefinedTypeKind.UnsafeArray;
					BitSize = null;
					Signed = null;
					break;
				default:
					throw new ArgumentException($"Invalid predefined type {syntax.Name.ValueText}", nameof(syntax));
			}
		}
	}
}
