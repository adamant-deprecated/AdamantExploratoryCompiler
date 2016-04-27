using Adamant.Exploratory.Compiler.Semantics.Types;
using Adamant.Exploratory.Compiler.Syntax.Expressions;

namespace Adamant.Exploratory.Compiler.Semantics.Expressions
{
	public class Cast : Expression
	{
		public new CastSyntax Syntax => (CastSyntax)base.Syntax;
		public Expression Expression { get; }
		public CastType CastType { get; }
		public ValueType Type { get; }

		public Cast(CastSyntax syntax, Package containingPackage, Expression expression, CastType castType, ValueType type)
			: base(syntax, containingPackage)
		{
			Expression = expression;
			CastType = castType;
			Type = type;
		}
	}
}
