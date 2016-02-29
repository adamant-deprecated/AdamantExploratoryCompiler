using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Statements;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class StatementBuilder : Builder<Statement>
	{
		private readonly DeclarationBuilder build;

		public StatementBuilder(DeclarationBuilder build)
		{
			this.build = build;
		}

		public override Statement VisitVariableDeclarationStatement(AdamantParser.VariableDeclarationStatementContext context)
		{
			return context.localVariableDeclaration().Accept(this);
		}

		public override Statement VisitLocalVariableDeclaration(AdamantParser.LocalVariableDeclarationContext context)
		{
			return new VariableDeclarationStatement();
		}

		public override Statement VisitExpressionStatement(AdamantParser.ExpressionStatementContext context)
		{
			var expression = context.expression().Accept(build.Expression);
			return new ExpressionStatement(expression);
		}

		public override Statement VisitReturnStatement(AdamantParser.ReturnStatementContext context)
		{
			return new ReturnStatement(context.expression().Accept(build.Expression));
		}

		public override Statement VisitThrowStatement(AdamantParser.ThrowStatementContext context)
		{
			return new ThrowStatement(context.expression().Accept(build.Expression));
		}

		public override Statement VisitIfStatement(AdamantParser.IfStatementContext context)
		{
			// TODO implement
			return new IfStatement();
		}

		public override Statement VisitForeachStatement(AdamantParser.ForeachStatementContext context)
		{
			// TODO implement
			return new ForeachStatement();
		}
	}
}
