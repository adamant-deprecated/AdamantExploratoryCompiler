using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class SimpleNameBuilder : Builder<SimpleName>
	{
		public override SimpleName VisitIdentifierName(AdamantParser.IdentifierNameContext context)
		{
			return new IdentifierName(Identifier(context.identifier()));
		}

		public override SimpleName VisitGenericName(AdamantParser.GenericNameContext context)
		{
			return new GenericName(Identifier(context.identifier()));
		}
	}
}
