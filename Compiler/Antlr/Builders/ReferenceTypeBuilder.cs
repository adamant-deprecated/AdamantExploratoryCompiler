using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ReferenceTypeBuilder : Builder<ReferenceType>
	{
		private readonly IBuildContext build;

		public ReferenceTypeBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override ReferenceType VisitImmutableReferenceType(AdamantParser.ImmutableReferenceTypeContext context)
		{
			var valueType = context.valueType().Accept(build.ValueType);
			return new ReferenceType(null, false, valueType);
		}

		public override ReferenceType VisitMutableReferenceType(AdamantParser.MutableReferenceTypeContext context)
		{
			var valueType = context.valueType().Accept(build.ValueType);
			return new ReferenceType(null, true, valueType);
		}

		public override ReferenceType VisitOwnedImmutableReferenceType(AdamantParser.OwnedImmutableReferenceTypeContext context)
		{
			var valueType = context.valueType().Accept(build.ValueType);
			return new ReferenceType(true, false, valueType);
		}

		public override ReferenceType VisitOwnedMutableReferenceType(AdamantParser.OwnedMutableReferenceTypeContext context)
		{
			var valueType = context.valueType().Accept(build.ValueType);
			return new ReferenceType(true, true, valueType);
		}
	}
}
