using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public interface IBuildContext
	{
		ValueTypeBuilder ValueType { get; }
		StatementBuilder Statement { get; }
		ExpressionBuilder Expression { get; }
		MemberBuilder Member { get; }
		NameBuilder Name { get; }
		ReferenceTypeBuilder ReferenceType { get; }
		SimpleNameBuilder SimpleName { get; }

		IList<ParameterSyntax> Parameters(AdamantParser.ParameterListContext context);
	}
}
