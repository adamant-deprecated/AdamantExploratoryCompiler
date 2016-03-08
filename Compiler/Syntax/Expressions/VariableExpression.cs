using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class VariableExpression : Expression
	{
		public readonly Symbol Name;

		public VariableExpression(Symbol name)
		{
			Name = name;
		}

		public void Bind(NameScope scope)
		{
			var definition = scope.Lookup(Name, DefinitionKind.NamespaceOrType).Resolve();
			// TODO store the definition
		}
	}
}
