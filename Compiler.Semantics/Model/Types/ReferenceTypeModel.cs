using Adamant.Exploratory.Compiler.Semantics.Types;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Model.Types
{
	internal class ReferenceTypeModel : SemanticElementModel<ReferenceTypeSyntax>, ReferenceType
	{
		public bool IsOwned { get; }
		public bool IsMutable => Syntax.IsMutable;
		public ValueType<ValueTypeSyntax> Type { get;  }

		public ReferenceTypeModel(ReferenceTypeSyntax syntax, PackageModel containingPackage, ValueType<ValueTypeSyntax> type)
			: base(syntax, containingPackage)
		{
			IsOwned = syntax.IsOwned ?? false; // TODO actually infer ownership
			Type = type;
		}
	}
}
