using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Expressions;
using Adamant.Exploratory.Compiler.Syntax.Expressions.Literals;
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
			var name = Identifier(context.simpleName().Start);
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
			return new BooleanLiteralSyntax(context.GetText());
		}

		public override ExpressionSyntax VisitIntLiteralExpression(AdamantParser.IntLiteralExpressionContext context)
		{
			return new IntegerLiteralSyntax(context.GetText());
		}

		public override ExpressionSyntax VisitStringLiteralExpression(AdamantParser.StringLiteralExpressionContext context)
		{
			return new StringLiteralSyntax(context.GetText());
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

		public override ExpressionSyntax VisitCastExpression(AdamantParser.CastExpressionContext context)
		{
			var expression = context.expression().Accept(this);
			CastType castType;
			switch(context.@as.Type)
			{
				case AdamantParser.As:
					castType = CastType.Safe;
					break;
				case AdamantParser.AsPanic:
					castType = CastType.Panic;
					break;
				case AdamantParser.AsResult:
					castType = CastType.Result;
					break;
				default:
					throw new NotSupportedException("Unsupported cast type");

			}
			var type = context.valueType().Accept(build.ValueType);
			return new CastSyntax(expression, castType, type);
		}
	}
}
