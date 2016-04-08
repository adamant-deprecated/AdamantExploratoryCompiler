using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Expressions
{
	/// <summary>
	/// Note that a literal does not always correspond to a literal syntax because of negative
	/// numeric literals.
	/// </summary>
	public abstract class Literal : Expression
	{
		protected Literal(ExpressionSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
