using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Field : NamedMember
	{
		public readonly Accessibility Access;
		public readonly bool IsMutableBinding;
		public readonly ReferenceType Type;
		public readonly Expression InitExpression;

		public Field(Accessibility access, bool isMutableBinding, Token name, ReferenceType type, Expression initExpression)
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
