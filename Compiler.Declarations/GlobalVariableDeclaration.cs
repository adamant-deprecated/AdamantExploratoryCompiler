using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Declarations
{
	public class GlobalVariableDeclaration : Declaration
	{
		public readonly GlobalVariableSyntax Syntax;

		public GlobalVariableDeclaration(GlobalVariableSyntax syntax)
			: base(syntax.Name.ValueText)
		{
			Syntax = syntax;
		}
	}
}
