using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class VariableDeclaration : EntityDeclaration
	{
		public bool IsMutableBinding;
		public ReferenceType Type;
		public Expression InitExpression;

		public VariableDeclaration(
			AccessModifier access,
			bool isMutableBinding,
			Token name,
			ReferenceType type,
			Expression initExpression)
			: base(access, name)
		{
			IsMutableBinding = isMutableBinding;
			Type = type;
			InitExpression = initExpression;
		}
	}
}
