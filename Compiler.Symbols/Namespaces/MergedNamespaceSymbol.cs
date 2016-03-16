using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Symbols.Namespaces
{
	/// <summary>
	/// Represents the namespace as declared across multiple packages.  It merges the
	/// package namespaces together.  This is still relative to a particular package
	/// so that returned SymbolReferences indicate which symbols are in the same package.
	/// </summary>
	public class MergedNamespaceSymbol : NamespaceSymbol
	{
		private readonly List<PackageNamespaceSymbol> mergedNamespaces;
		private readonly Dictionary<string, IReadOnlyList<SymbolReference>> cache = new Dictionary<string, IReadOnlyList<SymbolReference>>();

		// TODO create a correct constructor
		public MergedNamespaceSymbol(
			PackageSymbol containingPackage,
			NamespaceSymbol containingNamespace,
			ICollection<PackageNamespaceSymbol> mergedNamespaces)
			: base(containingPackage, containingNamespace, mergedNamespaces.FirstOrDefault()?.Name ?? "") // We will check for no merged namespaces, give a placeholder name in that case
		{
			Requires.That(mergedNamespaces.Count > 0, nameof(mergedNamespaces), "Must be at least one namespace to merge");
			Requires.That(mergedNamespaces.Select(n => n.Name).Distinct().Count() == 1, nameof(mergedNamespaces), "Must all have same name");

			this.mergedNamespaces = mergedNamespaces.ToList();
		}

		public override IReadOnlyList<SymbolReference> GetMembers(string name)
		{
			IReadOnlyList<SymbolReference> members;
			if(!cache.TryGetValue(name, out members))
				cache.Add(name, members = GetMembersFromMergedNamespaces(name));

			return members;
		}

		private IReadOnlyList<SymbolReference> GetMembersFromMergedNamespaces(string name)
		{
			var lookup = mergedNamespaces.SelectMany(n => n.GetMembers(name).Select(CorrectReference))
				.ToLookup(r => r.Symbol is PackageNamespaceSymbol);

			var childSymbols = lookup[false].ToList();
			var childNamespaces = lookup[true].ToList();
			if(childNamespaces.Any())
			{
				var childNamespace = new MergedNamespaceSymbol(ContainingPackage, this, childNamespaces.Select(r => r.Symbol).Cast<PackageNamespaceSymbol>().ToList());
				childSymbols.Add(new SymbolReference(childNamespace, childNamespaces.Any(n => n.InSamePackage)));
			}

			return childSymbols;
		}

		private SymbolReference CorrectReference(SymbolReference reference)
		{
			if(reference.Symbol.ContainingPackage == ContainingPackage)
				return reference;

			return new SymbolReference(reference.Symbol, false);
		}
	}
}
