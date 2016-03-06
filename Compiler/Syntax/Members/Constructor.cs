using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Constructor : MethodLike
	{
		public Constructor(AccessModifier access, Symbol name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, parameters, body)
		{
			Name = name;
		}

		public Symbol Name { get; }
	}
}
