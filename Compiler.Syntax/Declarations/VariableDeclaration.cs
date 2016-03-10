using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class VariableDeclaration : EntityDeclaration
	{
		public bool IsMutableBinding;
		public OwnershipType Type;
		public Expression InitExpression;

		public VariableDeclaration(
			AccessModifier access,
			bool isMutableBinding,
			Token name,
			OwnershipType type,
			Expression initExpression)
			: base(access, name)
		{
			Requires.NotNull(type, nameof(type));

			IsMutableBinding = isMutableBinding;
			Type = type;
			InitExpression = initExpression;
		}
	}
}
