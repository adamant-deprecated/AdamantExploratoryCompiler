using Adamant.Exploratory.Compiler.Syntax.Expressions;

namespace Adamant.Exploratory.Compiler.Syntax.Visitors
{
	public interface IExpressionVisitor<in TParam, out TReturn>
	{
		TReturn VisitLiteral(LiteralExpression expression, TParam param);
		TReturn VisitNew(NewExpression newExpression, TParam param);
		TReturn VisitNewObject(NewObjectExpression expression, TParam param);
		TReturn VisitVariable(VariableExpression variableExpression, TParam param);
		TReturn VisitIf(IfExpression ifExpression, TParam param);
		TReturn VisitBinaryOperator(BinaryOperatorExpression binaryOperatorExpression, TParam param);
		TReturn VisitCall(CallExpression callExpression, TParam param);
		TReturn VisitMember(MemberExpression memberExpression, TParam param);
	}
}
