using Adamant.Exploratory.Compiler.Semantics.References;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Semantics.Types.Value
{
	public class IdentifierName : SimpleName
	{
		public new IdentifierNameSyntax Syntax => (IdentifierNameSyntax)base.Syntax;
		internal DeclarationReference Declaration { get; }

		internal IdentifierName(IdentifierNameSyntax syntax, Package containingPackage, DeclarationReference declaration)
			: base(syntax, containingPackage)
		{
			Declaration = declaration;
		}
	}
}
