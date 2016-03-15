using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Binders
{
	/// <summary>
	/// A container binder binds <see cref="NamespaceDeclaration"/>s and <see cref="CompilationUnit"/>s. 
	/// </summary>
	public class ContainerBinder : Binder
	{
		private readonly Binder containingScope;
		private readonly IReadOnlyList<ImportedSymbol> imports;
		private readonly NamespaceSymbol @namespace;

		public ContainerBinder(Binder containingScope, NamespaceSymbol @namespace)
		{
			this.containingScope = containingScope;
			this.@namespace = @namespace;
		}

		public ContainerBinder(Binder containingScope, IEnumerable<ImportedSymbol> imports)
		{
			this.containingScope = containingScope;
			this.imports = imports.ToList();
		}

		//private readonly ScopeWithUsingStatements containingScope;
		//private readonly NamespaceDefinition @namespace;

		//public ContainerBinder(ScopeWithUsingStatements containingScope, NamespaceDefinition @namespace, IEnumerable<Definition> usingDefinitions)
		//	: base(usingDefinitions)
		//{
		//	if(@namespace == null) throw new ArgumentNullException(nameof(@namespace));
		//	if(containingScope == null) throw new ArgumentNullException(nameof(containingScope));

		//	this.containingScope = containingScope;
		//	this.@namespace = @namespace;
		//}

		//public override GlobalScope Globals => containingScope.Globals;

		//public override SymbolDefinitions Lookup(Symbol name, DefinitionKind kind = DefinitionKind.Any)
		//{
		//	Definition definition;
		//	if(@namespace.Definitions.TryGetValue(name, out definition))
		//		return new SymbolDefinitions(name, new SymbolDefinition(definition, true));

		//	var usingDefinitions = LookupInUsingStatements(name, kind);
		//	if(usingDefinitions.HasAccessibleDefinitions())
		//		return usingDefinitions;

		//	var containingDefinitions = containingScope.Lookup(name, kind);
		//	return containingDefinitions.HasAccessibleDefinitions() || usingDefinitions.Count == 0
		//		? containingDefinitions
		//		: usingDefinitions;
		//}

		//public override Definition LookupInCurrentScopeOnly(Symbol name, DefinitionKind kind = DefinitionKind.Any)
		//{
		//	// TODO watch out for kind
		//	return @namespace.Definitions.TryGetValue(name);
		//}
	}
}
