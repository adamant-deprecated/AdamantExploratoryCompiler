using System;
using System.Numerics;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Expressions.Literals;

namespace Adamant.Exploratory.Compiler.Semantics.Expressions.Literals
{
	public class IntegerLiteral : Literal
	{
		public BigInteger Value { get; }

		public IntegerLiteral(ExpressionSyntax syntax, Package containingPackage)
			: base(syntax, containingPackage)
		{
			Value = syntax.Match().Returning<BigInteger>()
				.With<IntegerLiteralSyntax>(literal => literal.Value)
				.Any(() =>
				{
					throw new ArgumentException("Must be an IntegerLiteralSyntax or a negated one", nameof(syntax));
				});
		}
	}
}
