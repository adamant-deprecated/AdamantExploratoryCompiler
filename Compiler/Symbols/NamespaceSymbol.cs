using System.Collections.Generic;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class NamespaceSymbol : DeclarationSymbol
	{
		private readonly MultiDictionary<string, DeclarationSymbol> members = new MultiDictionary<string, DeclarationSymbol>();

		/// <summary>
		/// Makes a new global namespace
		/// </summary>
		public NamespaceSymbol(PackageSymbol containingPackage)
			: base(containingPackage, null, "")
		{
		}

		public NamespaceSymbol(PackageSymbol containingPackage, NamespaceSymbol containingNamespace, string name)
			: base(containingPackage, containingNamespace, name)
		{
		}

		public IReadOnlyList<DeclarationSymbol> GetMembers(string name)
		{
			return members[name];
		}

		public bool IsGlobalNamespace => Name.Length == 0;

		public void Add(DeclarationSymbol declarationSymbol)
		{
			Requires.NotNull(declarationSymbol, nameof(declarationSymbol));
			Requires.That(declarationSymbol.ContainingNamespace == this, nameof(declarationSymbol), "Must be contained in this namespace");
			members.Add(declarationSymbol.Name, declarationSymbol);
		}
	}
}
