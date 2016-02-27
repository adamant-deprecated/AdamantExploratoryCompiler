using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations
{
	public class NamespaceDeclaration : ScopeDeclaration
	{
		public NamespaceDeclaration(FullyQualifiedName @namespace, IEnumerable<Declaration> declarations)
			: base(@namespace, declarations)
		{
		}
	}
}
