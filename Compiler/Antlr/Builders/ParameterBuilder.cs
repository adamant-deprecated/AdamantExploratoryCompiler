using Adamant.Exploratory.Compiler.Ast.Declarations;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ParameterBuilder : Builder<Parameter>
	{
		private readonly DeclarationBuilder build;

		public ParameterBuilder(DeclarationBuilder build)
		{
			this.build = build;
		}

		public override Parameter VisitParameter(AdamantParser.ParameterContext context)
		{
			// TODO modifiers
			// TODO this parameter
			var name = context.name.GetText();
			var type = context.type.Accept(build.Type);
			return new Parameter(name, type);
		}
	}
}
