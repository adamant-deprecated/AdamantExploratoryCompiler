using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Ast.Declarations;
using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast.Members
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
