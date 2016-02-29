using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public interface IBuildContext
	{
		FullyQualifiedName CurrentNamespace { get; }

		TypeBuilder Type { get; }
		StatementBuilder Statement { get; }
		ExpressionBuilder Expression { get; }
		MemberBuilder Member { get; }

		IList<Parameter> Parameters(AdamantParser.ParameterListContext context);
	}
}
