using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Types;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class GlobalDeclaration : Declaration
	{
		public GlobalDeclaration(
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

		public override TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param)
		{
			return visitor.VisitGlobalDeclaration(this, param);
		}
	}
}
