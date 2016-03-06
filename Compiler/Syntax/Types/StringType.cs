using System;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;

namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class StringType : PlainType
	{
		private static readonly Symbol SystemSymbol = new Symbol("System");
		private static readonly Symbol RuntimeSymbol = new Symbol("Runtime");
		private static readonly Symbol StringSymbol = new Symbol("string");

		private ClassDeclaration stringClass;

		public void Bind(NameScope scope)
		{
			if(stringClass != null) throw new NotSupportedException("Can't Bind() StringType more than once");

			stringClass = (ClassDeclaration)scope.Globals
				.LookupInPackage(SystemSymbol, "System.Runtime").Lookup(RuntimeSymbol).Lookup(StringSymbol)
				.Single().Definition;
		}
	}
}
