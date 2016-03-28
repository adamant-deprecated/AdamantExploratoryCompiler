using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Statements;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class StatementBuilder : Builder<StatementSyntax>
	{
		private readonly DeclarationBuilder build;

		public StatementBuilder(DeclarationBuilder build)
		{
			this.build = build;
		}

		public override StatementSyntax VisitVariableDeclarationStatement(AdamantParser.VariableDeclarationStatementContext context)
		{
			return context.localVariableDeclaration().Accept(this);
		}

		public override StatementSyntax VisitLocalVariableDeclaration(AdamantParser.LocalVariableDeclarationContext context)
		{
			return new LocalVariableSyntax();
		}

		public override StatementSyntax VisitExpressionStatement(AdamantParser.ExpressionStatementContext context)
		{
			var expression = context.expression().Accept(build.Expression);
			return new ExpressionStatementSyntax(expression);
		}

		public override StatementSyntax VisitReturnStatement(AdamantParser.ReturnStatementContext context)
		{
			return new ReturnSyntax(context.expression().Accept(build.Expression));
		}

		public override StatementSyntax VisitThrowStatement(AdamantParser.ThrowStatementContext context)
		{
			return new ThrowSyntax(context.expression().Accept(build.Expression));
		}

		public override StatementSyntax VisitIfStatement(AdamantParser.IfStatementContext context)
		{
			// TODO implement
			return new IfSyntax();
		}

		public override StatementSyntax VisitForeachStatement(AdamantParser.ForeachStatementContext context)
		{
			// TODO implement
			return new ForeachSyntax();
		}
	}
}
