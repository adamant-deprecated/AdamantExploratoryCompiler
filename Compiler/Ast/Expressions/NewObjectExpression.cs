using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Ast.Expressions
{
	public class NewObjectExpression : Expression
	{
		private readonly IList<Type> interfaces;
		public Type BaseClass { get;  }
		private readonly IList<Member> members;

		public NewObjectExpression(Type baseClass, IEnumerable<Type> interfaces, IEnumerable<Member> members)
		{
			this.interfaces = interfaces.ToList();
			BaseClass = baseClass;
			this.members = members.ToList();
		}

		public IEnumerable<Member> Members => members;

		public override TReturn Accept<TParam, TReturn>(IExpressionVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitNewObject(this, param);
		}
	}
}
