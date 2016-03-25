namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class StringTypeSyntax : ValueTypeSyntax
	{
		//private static readonly Symbol SystemSymbol = new Symbol("System");
		//private static readonly Symbol RuntimeSymbol = new Symbol("Runtime");
		//private static readonly Symbol StringSymbol = new Symbol("string");

		//private ClassDeclaration stringClass;

		//public void Bind(NameScope scope)
		//{
		//	if(stringClass != null) throw new NotSupportedException("Can't Bind() StringType more than once");

		//	stringClass = (ClassDeclaration)scope.Globals
		//		.LookupInPackage(SystemSymbol, "System.Runtime").Lookup(RuntimeSymbol).Lookup(StringSymbol)
		//		.Single().Definition;
		//}
		public readonly Token Token;

		public StringTypeSyntax(Token token)
		{
			Token = token;
		}
	}
}
