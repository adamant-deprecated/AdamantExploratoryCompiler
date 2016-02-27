using Adamant.Exploratory.Compiler.Syntax.Members;

namespace Adamant.Exploratory.Compiler.Syntax.Visitors
{
	public interface IMemberVisitor<in TParam, out TReturn>
	{
		TReturn VisitConstructor(Constructor member, TParam param);
		TReturn VisitField(Field member, TParam param);
		TReturn VisitMethod(Method member, TParam param);
		TReturn VisitProperty(Property member, TParam param);
	}
}
