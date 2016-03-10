﻿using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ParameterBuilder : Builder<Parameter>
	{
		private readonly DeclarationBuilder build;

		public ParameterBuilder(DeclarationBuilder build)
		{
			this.build = build;
		}

		public override Parameter VisitNamedParameter(AdamantParser.NamedParameterContext context)
		{
			// TODO modifiers
			// TODO this parameter
			var name = Identifier(context.name);
			var type = context.type.Accept(build.Type);
			return new Parameter(name, type);
		}

		public override Parameter VisitSelfParameter(AdamantParser.SelfParameterContext context)
		{
			// TODO modifiers
			// TODO this parameter
			var name = Identifier(context.name);
			return new Parameter(name, null);
		}
	}
}
