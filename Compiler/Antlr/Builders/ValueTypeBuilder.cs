using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ValueTypeBuilder : Builder<ValueType>
	{
		private readonly IBuildContext build;

		public ValueTypeBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override ValueType VisitNamedType(AdamantParser.NamedTypeContext context)
		{
			return context.name().Accept(build.Name);
		}

		public override ValueType VisitStringType(AdamantParser.StringTypeContext context)
		{
			return new StringType(Identifier(context.token));
		}

		public override ValueType VisitPrimitiveNumericType(AdamantParser.PrimitiveNumericTypeContext context)
		{
			return new NumericType(Identifier(context.token));
		}

		public override ValueType VisitMaybeType(AdamantParser.MaybeTypeContext context)
		{
			// TODO return a generic type Maybe<T>
			return context.valueType().Accept(this);
		}
	}
}
