using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class FunctionDeclaration : EntityDeclaration
	{
		public readonly IReadOnlyList<Parameter> Parameters;
		public readonly ReferenceType ReturnType;
		public readonly IReadOnlyList<Statement> Body;

		public FunctionDeclaration(
			Accessibility accessibility,
			Token name,
			IEnumerable<Parameter> parameters,
			ReferenceType returnType,
			IEnumerable<Statement> body)
			: base(accessibility, name)
		{
			Parameters = parameters.ToList();
			ReturnType = returnType;
			Body = body.ToList();
		}
	}
}
