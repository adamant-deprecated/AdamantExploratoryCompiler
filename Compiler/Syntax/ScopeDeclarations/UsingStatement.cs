using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations
{
	public class UsingStatement : SyntaxTree
	{
		public readonly TextPosition Position;
		public readonly FullyQualifiedName UsingName;

		public UsingStatement(TextPosition position, FullyQualifiedName usingName)
		{
			if(usingName == null) throw new ArgumentNullException(nameof(usingName));
			Position = position;
			UsingName = usingName;
		}
	}
}
