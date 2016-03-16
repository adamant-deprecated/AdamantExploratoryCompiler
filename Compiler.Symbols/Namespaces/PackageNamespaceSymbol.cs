using System.Collections.Generic;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Symbols.Namespaces
{
	/// <summary>
	/// A namespace as declared in a single package
	/// </summary>
	public class PackageNamespaceSymbol : NamespaceSymbol
	{
		private readonly MultiDictionary<string, SymbolReference> members = new MultiDictionary<string, SymbolReference>();

		/// <summary>
		/// Makes a new global namespace
		/// </summary>
		public PackageNamespaceSymbol(PackageSymbol containingPackage)
			: base(containingPackage, null, "")
		{
		}

		public PackageNamespaceSymbol(PackageSymbol containingPackage, PackageNamespaceSymbol containingNamespace, string name)
			: base(containingPackage, containingNamespace, name)
		{
		}

		public override IReadOnlyList<SymbolReference> GetMembers(string name)
		{
			return members[name];
		}

		public void Add(DeclarationSymbol declarationSymbol)
		{
			Requires.NotNull(declarationSymbol, nameof(declarationSymbol));
			Requires.That(declarationSymbol.ContainingNamespace == this, nameof(declarationSymbol), "Must be contained in this namespace");
			members.Add(declarationSymbol.Name, new SymbolReference(declarationSymbol, true));
		}
	}
}
