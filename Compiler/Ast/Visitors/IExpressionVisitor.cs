using Adamant.Exploratory.Compiler.Ast.Expressions;

namespace Adamant.Exploratory.Compiler.Ast.Visitors
{
	public interface IExpressionVisitor<in TParam, out TReturn>
	{
		TReturn VisitLiteral(LiteralExpression expression, TParam param);
		TReturn VisitNewObject(NewObjectExpression expression, TParam param);
	}
}
