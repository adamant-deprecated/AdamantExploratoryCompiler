using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ParameterBuilder : Builder<Parameter>
	{
		private readonly IBuildContext build;

		public ParameterBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override Parameter VisitNamedParameter(AdamantParser.NamedParameterContext context)
		{
			// TODO modifiers
			// TODO this parameter
			var name = Identifier(context.identifier());
			var type = context.referenceType().Accept(build.ReferenceType);
			return new Parameter(name, type);
		}

		public override Parameter VisitSelfParameter(AdamantParser.SelfParameterContext context)
		{
			// TODO modifiers
			// TODO this parameter
			var name = Identifier(context.token);
			return new Parameter(name, null);
		}
	}
}
