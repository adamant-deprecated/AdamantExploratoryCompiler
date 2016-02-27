using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Syntax.Visitors
{
	public interface IDeclarationVisitor<in TParam, out TReturn>
	{
		TReturn VisitClassDeclaration(ClassDeclaration declaration, TParam param);
		TReturn VisitFunctionDeclaration(FunctionDeclaration declaration, TParam param);
		TReturn VisitGlobalDeclaration(GlobalDeclaration declaration, TParam param);
	}
}
