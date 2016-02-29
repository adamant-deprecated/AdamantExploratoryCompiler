using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.EntityDeclarations
{
	public class VariableDeclaration : EntityDeclaration
	{
		public VariableDeclaration(
			AccessModifier access,
			bool isMutableReference,
			FullyQualifiedName @namespace,
			Symbol name,
			OwnershipType type,
			Expression initExpression)
			: base(access, @namespace, name)
		{
			IsMutableReference = isMutableReference;
			Type = type;
			InitExpression = initExpression;
		}

		public bool IsMutableReference { get; }
		public OwnershipType Type { get; }
		public Expression InitExpression { get; }
	}
}
