using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ParameterBuilder : Builder<ParameterSyntax>
	{
		private readonly IBuildContext build;

		public ParameterBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override ParameterSyntax VisitNamedParameter(AdamantParser.NamedParameterContext context)
		{
			// TODO modifiers
			// TODO this parameter
			var name = Identifier(context.identifier());
			var type = context.referenceType().Accept(build.ReferenceType);
			return new ParameterSyntax(name, type);
		}

		public override ParameterSyntax VisitSelfParameter(AdamantParser.SelfParameterContext context)
		{
			// TODO modifiers
			// TODO this parameter
			var name = Identifier(context.token);
			return new ParameterSyntax(name, null);
		}
	}
}
