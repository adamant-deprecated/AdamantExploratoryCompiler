using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Expressions;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class ExpressionBuilder : Builder<ExpressionSyntax>
	{
		private readonly IBuildContext build;

		public ExpressionBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override ExpressionSyntax VisitMemberExpression(AdamantParser.MemberExpressionContext context)
		{
			var expression = context.expression().Accept(this);
			var member = Identifier(context.identifier());
			return new MemberAccessSyntax(expression, member);
		}

		public override ExpressionSyntax VisitCallExpression(AdamantParser.CallExpressionContext context)
		{
			var expression = context.expression().Accept(this);
			var arguments = context.argumentList()._expressions.Select(exp => exp.Accept(this));
			return new CallSyntax(expression, arguments);
		}

		public override ExpressionSyntax VisitEqualityExpression(AdamantParser.EqualityExpressionContext context)
		{
			var lhs = context.lhs.Accept(this);
			var rhs = context.rhs.Accept(this);
			return new BinaryOperationSyntax(lhs, rhs);
		}

		public override ExpressionSyntax VisitIfExpression(AdamantParser.IfExpressionContext context)
		{
			var condition = context.condition.Accept(this);
			var then = context.then.Accept(this);
			var @else = context.@else.Accept(this);
			return new ConditionalSyntax(condition, then, @else);
		}

		public override ExpressionSyntax VisitNameExpression(AdamantParser.NameExpressionContext context)
		{
			var name = Identifier(context.identifier());
			return new IdentifierNameSyntax(name);
		}

		public override ExpressionSyntax VisitNewExpression(AdamantParser.NewExpressionContext context)
		{
			var type = context.name().Accept(build.Name);
			var arguments = context.argumentList()._expressions.Select(exp => exp.Accept(this));
			return new NewSyntax(type, arguments);
		}

		public override ExpressionSyntax VisitNewObjectExpression(AdamantParser.NewObjectExpressionContext context)
		{
			var baseTypes = context.baseTypes();
			var baseClass = baseTypes?.baseType?.Accept(build.ValueType);
			var interfaces = baseTypes?._interfaces.Select(i => i.Accept(build.ValueType)).ToList() ?? new List<ValueTypeSyntax>();
			var arguments = context.argumentList()._expressions.Select(exp => exp.Accept(this));
			var members = context.member().Select(m => m.Accept(build.Member));
			return new NewAnonymousObjectSyntax(baseClass, interfaces, arguments, members);
		}

		public override ExpressionSyntax VisitBooleanLiteralExpression(AdamantParser.BooleanLiteralExpressionContext context)
		{
			return new LiteralSyntax();
		}

		public override ExpressionSyntax VisitIntLiteralExpression(AdamantParser.IntLiteralExpressionContext context)
		{
			return new LiteralSyntax();
		}

		public override ExpressionSyntax VisitStringLiteralExpression(AdamantParser.StringLiteralExpressionContext context)
		{
			return new LiteralSyntax();
		}

		public override ExpressionSyntax VisitAssignmentExpression(AdamantParser.AssignmentExpressionContext context)
		{
			var lvalue = context.lvalue.Accept(this);
			var rvalue = context.rvalue.Accept(this);
			return new AssignmentSyntax(lvalue, rvalue);
		}

		public override ExpressionSyntax VisitSelfExpression(AdamantParser.SelfExpressionContext context)
		{
			return new SelfSyntax();
		}
	}
}
