using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Types
{
	public class TypeName : PlainType
	{
		public TypeName OuterType;
		public Token Name;

		public TypeName(TypeName outerType, Token name)
		{
			Requires.NotNull(outerType, nameof(outerType));
			SyntaxRequires.TypeIs(name, TokenType.Identifier, nameof(name));

			OuterType = outerType;
			Name = name;
		}


		//public Definition Definition { get; private set; }

		//public void Bind(NameScope scope)
		//{
		//	if(OuterType != null)
		//	{
		//		OuterType.Bind(scope);
		//		var definition = OuterType.Definition.Definitions.TryGetValue(Name);
		//		if(definition == null) throw new Exception($"{Name} is not defined in scope");
		//		Definition = definition;
		//	}

		//	Definition = scope.Lookup(Name, DefinitionKind.NamespaceOrType).Resolve();
		//}
	}
}
