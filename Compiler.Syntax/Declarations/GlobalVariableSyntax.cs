using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	/// <summary>
	/// Represents a global variable declaration
	/// </summary>
	public class GlobalVariableSyntax : EntitySyntax
	{
		public bool IsMutableBinding;
		public ReferenceTypeSyntax Type;
		public ExpressionSyntax InitExpression;

		public GlobalVariableSyntax(
			Accessibility accessibility,
			bool isMutableBinding,
			Token name,
			ReferenceTypeSyntax type,
			ExpressionSyntax initExpression)
			: base(accessibility, name)
		{
			IsMutableBinding = isMutableBinding;
			Type = type;
			InitExpression = initExpression;
		}
	}
}
