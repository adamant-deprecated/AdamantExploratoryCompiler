using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Method : MethodLike
	{
		public Method(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, parameters, body)
		{
		}

		public override TReturn Accept<TParam, TReturn>(IMemberVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitMethod(this, param);
		}
	}
}
