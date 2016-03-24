using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Binders.SymbolReferences
{
	/// <summary>
	/// Due to the nature of namespaces, a namespace reference spans ContainerSymbols
	/// across packages and merges them together.
	/// </summary>
	internal class NamespaceReference : SymbolReference
	{
		private readonly List<ContainerSymbol> containers;
		private readonly Dictionary<string, IReadOnlyList<SymbolReference>> cache = new Dictionary<string, IReadOnlyList<SymbolReference>>();

		public override string Name => containers.First().AsNamespace?.Name ?? "";

		public NamespaceReference(IEnumerable<ContainerSymbol> containers)
		{
			this.containers = containers.ToList();

			Requires.That(this.containers.Count > 0, nameof(containers), "Must be at least one container to merge");
		}

		public override bool IsIn(Package package)
		{
			return containers.Any(c => c.ContainingPackage == package);
		}

		public override bool IsVisibleFrom(Package package)
		{
			// Namespaces are always public
			return true;
		}

		public IEnumerable<SymbolReference> GetMembers()
		{
			return containers.SelectMany(c => c.GetMembers()).GroupBy(m => m.Name).SelectMany(g =>
			{
				IReadOnlyList<SymbolReference> members;
				if(!cache.TryGetValue(g.Key, out members))
					cache.Add(g.Key, members = GetMembersSlow(g));

				return members;
			});
		}

		public override IEnumerable<SymbolReference> GetMembers(string name)
		{
			IReadOnlyList<SymbolReference> members;
			if(!cache.TryGetValue(name, out members))
				cache.Add(name, members = GetMembersSlow(name));

			return members;
		}

		private IReadOnlyList<SymbolReference> GetMembersSlow(string name)
		{
			return GetMembersSlow(containers.SelectMany(n => n.GetMembers(name)));
		}

		private static IReadOnlyList<SymbolReference> GetMembersSlow(IEnumerable<DeclarationSymbol> symbols)
		{
			var lookup = symbols
				.ToLookup(s => s is NamespaceSymbol);

			var childSymbols = lookup[false].Select(s => (SymbolReference)s).ToList();
			var childNamespaces = lookup[true].ToList();
			if(childNamespaces.Any())
			{
				var childNamespace = new NamespaceReference(childNamespaces.Cast<NamespaceSymbol>());
				childSymbols.Add(childNamespace);
			}

			return childSymbols;
		}
	}
}
