using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class Declaration : Node
	{
		protected Declaration(FullyQualifiedName @namespace)
		{
			Namespace = @namespace;
		}

		public FullyQualifiedName Namespace { get; }
	}
}
