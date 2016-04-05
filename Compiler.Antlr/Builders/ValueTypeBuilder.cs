using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ValueTypeBuilder : Builder<ValueTypeSyntax>
	{
		private readonly IBuildContext build;

		public ValueTypeBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override ValueTypeSyntax VisitNamedType(AdamantParser.NamedTypeContext context)
		{
			return context.name().Accept(build.Name);
		}

		public override ValueTypeSyntax VisitMaybeType(AdamantParser.MaybeTypeContext context)
		{
			// TODO return a generic type Maybe<T>
			return context.valueType().Accept(this);
		}
	}
}
