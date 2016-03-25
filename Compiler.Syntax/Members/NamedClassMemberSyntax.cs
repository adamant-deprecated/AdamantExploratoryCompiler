using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public abstract class NamedClassMemberSyntax : ClassMemberSyntax
	{
		public readonly Token Name;

		protected NamedClassMemberSyntax(Token name)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Name = name;
		}
	}
}
