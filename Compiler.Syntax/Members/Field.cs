using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Field : NamedMember
	{
		public readonly AccessModifier Access;
		public readonly bool IsMutableBinding;
		public readonly OwnershipType Type;
		public readonly Expression InitExpression;

		public Field(AccessModifier access, bool isMutableBinding, Token name, OwnershipType type, Expression initExpression)
			: base(name)
		{
			Requires.EnumDefined(access, nameof(access));
			Requires.NotNull(type, nameof(type));

			Access = access;
			IsMutableBinding = isMutableBinding;
			Type = type;
			InitExpression = initExpression;
		}
	}
}
