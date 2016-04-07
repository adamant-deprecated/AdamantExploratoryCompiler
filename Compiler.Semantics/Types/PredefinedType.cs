using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types
{
	public interface PredefinedType : ValueType<PredefinedTypeSyntax>
	{
		PredefinedTypeKind Kind { get; }
		int? BitSize { get; }
		int? FractionalLength { get; }
		bool? Signed { get; }
	}
}