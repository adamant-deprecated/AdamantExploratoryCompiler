using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Expressions;

namespace Adamant.Exploratory.Compiler.Semantics.Expressions
{
	public class Call : Expression
	{
		public new CallSyntax Syntax => (CallSyntax)base.Syntax;
		public Expression Expression { get; }
		public IReadOnlyList<Expression> Arguments { get; }

		public Call(CallSyntax syntax, Package containingPackage, Expression expression, IEnumerable<Expression> arguments)
			: base(syntax, containingPackage)
		{
			Expression = expression;
			Arguments = arguments.ToList();
		}
	}
}
