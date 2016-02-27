using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Constructor : MethodLike
	{
		public Constructor(AccessModifier access, Name name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, parameters, body)
		{
			Name = name;
		}

		public Name Name { get; }

		public override TReturn Accept<TParam, TReturn>(IMemberVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitConstructor(this, param);
		}
	}
}
