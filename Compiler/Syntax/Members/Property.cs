using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	// TODO should this be a separate node or just a special kind of method?
	public class Property : MethodLike
	{
		public Property(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access,parameters,body)
		{
		}

		public override TReturn Accept<TParam, TReturn>(IMemberVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitProperty(this, param);
		}
	}
}
