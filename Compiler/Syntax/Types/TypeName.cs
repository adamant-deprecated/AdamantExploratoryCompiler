using System;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class TypeName : PlainType
	{
		public TypeName(TypeName outerType, Symbol name)
		{
			if(name == null) throw new ArgumentNullException(nameof(name));
			OuterType = outerType;
			Name = name;
		}

		public TypeName OuterType { get; }
		public Symbol Name { get; }
		public Definition Definition { get; private set; }

		public void Bind(NameScope scope)
		{
			if(OuterType != null)
			{
				OuterType.Bind(scope);
				var definition = OuterType.Definition.Definitions.TryGetValue(Name);
				if(definition == null) throw new Exception($"{Name} is not defined in scope");
				Definition = definition;
			}

			Definition = scope.Lookup(Name, DefinitionKind.NamespaceOrType).Resolve();
		}
	}
}
