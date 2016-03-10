using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public abstract class EntityDeclaration : Declaration
	{
		public readonly AccessModifier Access;
		public readonly Token Name;

		protected EntityDeclaration(
			AccessModifier access,
			Token name)
		{
			Requires.EnumIn(access, nameof(access), AccessModifier.Public, AccessModifier.Package);
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Access = access;
			Name = name;
		}
	}
}
