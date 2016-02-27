using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Method : MethodLike
	{
		public Method(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access, parameters, body)
		{
		}
	}
}
