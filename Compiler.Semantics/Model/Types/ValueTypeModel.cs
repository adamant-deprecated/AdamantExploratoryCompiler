using Adamant.Exploratory.Compiler.Semantics.Types;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Model.Types
{
	internal class ValueTypeModel<TSyntax> : SemanticElementModel<TSyntax>, ValueType<TSyntax>
		where TSyntax : ValueTypeSyntax
	{
		ValueTypeSyntax ValueType<TSyntax>.Syntax => Syntax;

		public ValueTypeModel(TSyntax syntax, PackageModel containingPackage)
			: base(syntax, containingPackage)
		{
		}
	}
}
