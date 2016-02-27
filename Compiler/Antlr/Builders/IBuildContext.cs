using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public interface IBuildContext
	{
		UsingContext UsingContext { get; }
		QualifiedName CurrentNamespace { get; }

		TypeBuilder Type { get; }
		StatementBuilder Statement { get; }
		ExpressionBuilder Expression { get; }
		MemberBuilder Member { get; }

		IList<Parameter> Parameters(AdamantParser.ParameterListContext context);
	}
}
