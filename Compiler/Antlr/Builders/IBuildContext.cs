using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public interface IBuildContext
	{
		TypeBuilder Type { get; }
		StatementBuilder Statement { get; }
		ExpressionBuilder Expression { get; }
		MemberBuilder Member { get; }

		IList<Parameter> Parameters(AdamantParser.ParameterListContext context);
	}
}
