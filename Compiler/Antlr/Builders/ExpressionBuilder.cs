using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Expressions;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ExpressionBuilder : Builder<Expression>
	{
		private readonly DeclarationBuilder build;

		public ExpressionBuilder(DeclarationBuilder build)
		{
			this.build = build;
		}

		public override Expression VisitMemberExpression(AdamantParser.MemberExpressionContext context)
		{
			var expression = context.expression().Accept(this);
			var member = context.identifier().GetText();
			return new MemberExpression(expression, member);
		}

		public override Expression VisitCallExpression(AdamantParser.CallExpressionContext context)
		{
			var expression = context.expression().Accept(this);
			var arguments = context.argumentList()._expressions.Select(exp => exp.Accept(this));
			return new CallExpression(expression, arguments);
		}

		public override Expression VisitEqualityExpression(AdamantParser.EqualityExpressionContext context)
		{
			var lhs = context.lhs.Accept(this);
			var rhs = context.lhs.Accept(this);
			return new BinaryOperatorExpression(lhs, rhs);
		}

		public override Expression VisitIfExpression(AdamantParser.IfExpressionContext context)
		{
			var condition = context.condition.Accept(this);
			var then = context.then.Accept(this);
			var @else = context.@else.Accept(this);
			return new IfExpression(condition, then, @else);
		}

		public override Expression VisitVariableExpression(AdamantParser.VariableExpressionContext context)
		{
			var name = new Name(context.identifier().GetText());
			return new VariableExpression(name);
		}

		public override Expression VisitNewExpression(AdamantParser.NewExpressionContext context)
		{
			var type = (TypeName)context.typeName().Accept(build.Type);
			var arguments = context.argumentList()._expressions.Select(exp => exp.Accept(this));
			return new NewExpression(type, arguments);
		}

		public override Expression VisitNewObjectExpression(AdamantParser.NewObjectExpressionContext context)
		{
			var baseTypes = context.baseTypes();
			var baseClass = baseTypes?.baseType?.Accept(build.Type);
			var interfaces = baseTypes?._interfaces.Select(i => i.Accept(build.Type)).ToList() ?? new List<Type>();
			var arguments = context.argumentList()._expressions.Select(exp => exp.Accept(this));
			var members = context.member().Select(m => m.Accept(build.Member));
			return new NewObjectExpression(baseClass, interfaces, arguments, members);
		}

		public override Expression VisitBooleanLiteralExpression(AdamantParser.BooleanLiteralExpressionContext context)
		{
			return new LiteralExpression();
		}

		public override Expression VisitIntLiteralExpression(AdamantParser.IntLiteralExpressionContext context)
		{
			return new LiteralExpression();
		}

		public override Expression VisitStringLiteralExpression(AdamantParser.StringLiteralExpressionContext context)
		{
			return new LiteralExpression();
		}
	}
}
