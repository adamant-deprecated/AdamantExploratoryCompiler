using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class FunctionDeclaration : EntityDeclaration
	{
		public readonly IReadOnlyList<Parameter> Parameters;
		public readonly Type ReturnType;
		public readonly IReadOnlyList<Statement> Body;

		public FunctionDeclaration(
			AccessModifier access,
			Token name,
			IEnumerable<Parameter> parameters,
			Type returnType,
			IEnumerable<Statement> body)
			: base(access, name)
		{
			Parameters = parameters.ToList();
			ReturnType = returnType;
			Body = body.ToList();
		}
	}
}
