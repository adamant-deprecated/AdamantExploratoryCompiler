using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class NamespaceDefinition : Definition
	{
		public NamespaceDefinition(FullyQualifiedName @namespace, Symbol name)
		{
			Namespace = @namespace;
			Name = name;
			FullyQualifiedName = @namespace.Append(name);
			Definitions = new Definitions();
		}

		public FullyQualifiedName Namespace { get; }
		public Symbol Name { get; }
		public FullyQualifiedName FullyQualifiedName { get; }
		public Definitions Definitions { get;  }
	}
}
