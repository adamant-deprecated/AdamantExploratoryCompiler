namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public abstract class NamedMember : Member
	{
		public readonly Token Name;

		protected NamedMember(Token name)
		{
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			Name = name;
		}
	}
}
