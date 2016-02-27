using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.Visitors
{
	public interface ITypeVisitor<in TParam, out TReturn>
	{
		TReturn VisitInferredType(InferredType type, TParam param);
		TReturn VisitOwnershipType(OwnershipType type, TParam param);
		TReturn VisitTypeName(TypeName type, TParam param);
		TReturn VisitArraySliceType(ArraySliceType type, TParam param);
	}
}
