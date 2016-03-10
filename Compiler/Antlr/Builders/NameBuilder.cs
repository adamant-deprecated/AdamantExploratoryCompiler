using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class NameBuilder : Builder<Name>
	{
		private readonly IBuildContext build;

		public NameBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override Name VisitSimpleNameName(AdamantParser.SimpleNameNameContext context)
		{
			return context.simpleName().Accept(build.SimpleName);
		}

		public override Name VisitQualifiedName(AdamantParser.QualifiedNameContext context)
		{
			var left = context.leftName.Accept(this);
			var right = context.rightName.Accept(build.SimpleName);
			return new QualifiedName(left, right);
		}
	}
}
