namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class CastSyntax : ExpressionSyntax
	{
		public readonly ExpressionSyntax Expression;
		public readonly CastType CastType;
		public readonly ValueTypeSyntax Type;

		public CastSyntax(ExpressionSyntax expression, CastType castType, ValueTypeSyntax type)
		{
			Expression = expression;
			CastType = castType;
			Type = type;
		}
	}
}
