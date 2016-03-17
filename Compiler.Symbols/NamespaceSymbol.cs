using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class NamespaceSymbol : DeclarationSymbol, ContainerSymbol
	{
		private readonly MultiDictionary<string, DeclarationSymbol> members = new MultiDictionary<string, DeclarationSymbol>();

		NamespaceSymbol ContainerSymbol.AsNamespace => this;

		public NamespaceSymbol(Package containingPackage, string name, IEnumerable<DeclarationSymbol> declarations)
			: base(containingPackage, Accessibility.Public, name) // namespaces are implicitly public
		{
			foreach(var declaration in declarations)
				members.Add(declaration.Name, declaration);
		}

		protected override IReadOnlyList<Symbol> GetMembersInternal(string name)
		{
			return members[name];
		}

		public IEnumerable<DeclarationSymbol> GetMembers()
		{
			return members.Values;
		}

		public new IReadOnlyList<DeclarationSymbol> GetMembers(string name)
		{
			return members[name];
		}
	}
}
