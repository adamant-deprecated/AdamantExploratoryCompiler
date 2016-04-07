using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class FunctionSyntax : EntitySyntax
	{
		public readonly IReadOnlyList<ParameterSyntax> Parameters;
		public readonly ReferenceTypeSyntax ReturnType;
		public readonly IReadOnlyList<StatementSyntax> Body;

		public FunctionSyntax(
			Accessibility accessibility,
			SyntaxToken name,
			IEnumerable<ParameterSyntax> parameters,
			ReferenceTypeSyntax returnType,
			IEnumerable<StatementSyntax> body)
			: base(accessibility, name)
		{
			Parameters = parameters.ToList();
			ReturnType = returnType;
			Body = body.ToList();
		}
	}
}
