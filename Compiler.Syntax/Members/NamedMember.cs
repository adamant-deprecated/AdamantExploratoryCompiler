using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public abstract class NamedMember : Member
	{
		public readonly Token Name;

		protected NamedMember(Token name)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Name = name;
		}
	}
}
