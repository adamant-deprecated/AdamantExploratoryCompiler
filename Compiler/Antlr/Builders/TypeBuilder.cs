using System;
using Adamant.Exploratory.Compiler.Syntax.Types;
using Type = Adamant.Exploratory.Compiler.Syntax.Type;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class TypeBuilder : Builder<Type>
	{
		public override Type VisitMutableType(AdamantParser.MutableTypeContext context)
		{
			var type = (PlainType)context.plainType().Accept(this);
			return new OwnershipType(Ownership.BorrowMutable, type);
		}

		public override Type VisitOwnedType(AdamantParser.OwnedTypeContext context)
		{
			var type = (PlainType)context.plainType().Accept(this);
			return new OwnershipType(Ownership.OwnedMutable, type);
		}

		public override Type VisitImmutableType(AdamantParser.ImmutableTypeContext context)
		{
			var type = (PlainType)context.plainType().Accept(this);
			return new OwnershipType(Ownership.BorrowImmutable, type);
		}

		public override Type VisitNamedType(AdamantParser.NamedTypeContext context)
		{
			return context.typeName().Accept(this);
		}

		public override Type VisitTypeName(AdamantParser.TypeNameContext context)
		{
			var outerType = (TypeName)context.outerType?.Accept(this);
			var name = Symbol(context.identifier());
			return new TypeName(outerType, name);
		}

		public override Type VisitArraySliceType(AdamantParser.ArraySliceTypeContext context)
		{
			var elementType = context.elementType.Accept(this);
			var dimensions = context._dimensions.Count + 1;
			return new ArraySliceType(elementType, dimensions);
		}

		public override Type VisitMaybeType(AdamantParser.MaybeTypeContext context)
		{
			// TODO return a generic type Maybe<T>
			return context.plainType().Accept(this);
		}

		public override Type VisitPrimitiveNumericType(AdamantParser.PrimitiveNumericTypeContext context)
		{
			return new NumericType(context.GetText());
		}

		public override Type VisitStringType(AdamantParser.StringTypeContext context)
		{
			return new StringType();
		}
	}
}
