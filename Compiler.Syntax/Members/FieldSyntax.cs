using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class FieldSyntax : NamedClassMemberSyntax
	{
		public readonly Accessibility Access;
		public readonly bool IsMutableBinding;
		public readonly ReferenceTypeSyntax Type;
		public readonly ExpressionSyntax InitExpression;

		public FieldSyntax(Accessibility access, bool isMutableBinding, SyntaxToken name, ReferenceTypeSyntax type, ExpressionSyntax initExpression)
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
