using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class SimpleNameBuilder : Builder<SimpleNameSyntax>
	{
		public override SimpleNameSyntax VisitIdentifierName(AdamantParser.IdentifierNameContext context)
		{
			return new IdentifierNameSyntax(Identifier(context.identifier()));
		}

		public override SimpleNameSyntax VisitGenericName(AdamantParser.GenericNameContext context)
		{
			return new GenericNameSyntax(Identifier(context.identifier()));
		}
	}
}
