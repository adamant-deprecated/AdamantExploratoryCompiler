using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public abstract class EntityDeclaration : Declaration
	{
		public readonly Accessibility Accessibility;
		public readonly Token Name;

		protected EntityDeclaration(
			Accessibility accessibility,
			Token name)
		{
			Requires.EnumIn(accessibility, nameof(accessibility), Accessibility.Public, Accessibility.Package);
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Accessibility = accessibility;
			Name = name;
		}
	}
}
