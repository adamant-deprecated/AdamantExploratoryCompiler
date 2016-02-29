using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations
{
	public class NamespaceDeclaration : ScopeWithUsingStatements, Declaration
	{
		public NamespaceDeclaration(FullyQualifiedName @namespace, FullyQualifiedName name, IEnumerable<UsingStatement> usingStatements, IEnumerable<Declaration> declarations)
			: base(usingStatements, declarations)
		{
			Namespace = @namespace;
			Name = name;
			FullyQualifiedName = @namespace.Append(name);
		}

		public FullyQualifiedName Namespace { get; }
		public FullyQualifiedName Name { get; }
		public FullyQualifiedName FullyQualifiedName { get; }
	}
}
