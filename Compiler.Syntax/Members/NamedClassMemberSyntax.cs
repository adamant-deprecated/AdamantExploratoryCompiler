using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public abstract class NamedClassMemberSyntax : ClassMemberSyntax
	{
		public readonly SyntaxToken Name;

		protected NamedClassMemberSyntax(SyntaxToken name)
		{
			Requires.NotNull(name, nameof(name));
			SyntaxRequires.TypeIs(name, SyntaxTokenType.Identifier, nameof(name));

			Name = name;
		}
	}
}
