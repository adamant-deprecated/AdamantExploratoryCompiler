using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ReferenceTypeBuilder : Builder<ReferenceTypeSyntax>
	{
		private readonly IBuildContext build;

		public ReferenceTypeBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override ReferenceTypeSyntax VisitImmutableReferenceType(AdamantParser.ImmutableReferenceTypeContext context)
		{
			var valueType = context.valueType().Accept(build.ValueType);
			return new ReferenceTypeSyntax(null, false, valueType);
		}

		public override ReferenceTypeSyntax VisitMutableReferenceType(AdamantParser.MutableReferenceTypeContext context)
		{
			var valueType = context.valueType().Accept(build.ValueType);
			return new ReferenceTypeSyntax(null, true, valueType);
		}

		public override ReferenceTypeSyntax VisitOwnedImmutableReferenceType(AdamantParser.OwnedImmutableReferenceTypeContext context)
		{
			var valueType = context.valueType().Accept(build.ValueType);
			return new ReferenceTypeSyntax(true, false, valueType);
		}

		public override ReferenceTypeSyntax VisitOwnedMutableReferenceType(AdamantParser.OwnedMutableReferenceTypeContext context)
		{
			var valueType = context.valueType().Accept(build.ValueType);
			return new ReferenceTypeSyntax(true, true, valueType);
		}
	}
}
