using Adamant.Exploratory.Compiler.Ast.Members;

namespace Adamant.Exploratory.Compiler.Ast.Visitors
{
	public interface IMemberVisitor<in TParam, out TReturn>
	{
		TReturn VisitConstructor(Constructor member, TParam param);
		TReturn VisitField(Field member, TParam param);
		TReturn VisitMethod(Method member, TParam param);
		TReturn VisitProperty(Property member, TParam param);
	}
}
