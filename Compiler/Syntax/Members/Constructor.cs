using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Constructor : MethodLike
	{
		public Constructor(AccessModifier access, Symbol symbol, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, parameters, body)
		{
			Symbol = symbol;
		}

		public Symbol Symbol { get; }

		public override TReturn Accept<TParam, TReturn>(IMemberVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitConstructor(this, param);
		}
	}
}
