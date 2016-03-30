using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Semantics.References
{
	/// <summary>
	/// Due to the nature of namespaces, a namespace reference spans ContainerSymbols
	/// across packages and merges them together.
	/// </summary>
	internal class NamespaceReference : DeclarationReference
	{
		private readonly List<Namespace> namespaces;
		private readonly Dictionary<string, IReadOnlyList<DeclarationReference>> cache = new Dictionary<string, IReadOnlyList<DeclarationReference>>();

		public override string Name => namespaces.First().Name;

		public NamespaceReference(IEnumerable<Namespace> namespaces)
		{
			this.namespaces = namespaces.ToList();

			Requires.That(this.namespaces.Count > 0, nameof(namespaces), "Must be at least one container to merge");
		}

		public override bool IsIn(Package package)
		{
			return namespaces.Any(c => c.ContainingPackage == package);
		}

		public override bool IsVisibleFrom(Package package)
		{
			// Namespaces are always public
			return true;
		}

		public IEnumerable<DeclarationReference> GetMembers()
		{
			return namespaces.SelectMany(ns => ns.GetMembers()).GroupBy(m => m.Name).SelectMany(g =>
			{
				IReadOnlyList<DeclarationReference> members;
				if(!cache.TryGetValue(g.Key, out members))
					cache.Add(g.Key, members = GetMembersSlow(g));

				return members;
			});
		}

		public override IEnumerable<DeclarationReference> GetMembers(string name)
		{
			IReadOnlyList<DeclarationReference> members;
			if(!cache.TryGetValue(name, out members))
				cache.Add(name, members = GetMembersSlow(name));

			return members;
		}

		private IReadOnlyList<DeclarationReference> GetMembersSlow(string name)
		{
			return GetMembersSlow(namespaces.SelectMany(ns => ns.GetMembers(name)));
		}

		private static IReadOnlyList<DeclarationReference> GetMembersSlow(IEnumerable<Declaration<DeclarationSyntax>> symbols)
		{
			var lookup = symbols
				.ToLookup(s => s is Namespace);

			var childSymbols = lookup[false].Cast<Entity<EntitySyntax>>()
				.Select(e => (DeclarationReference)new EntityReference(e)).ToList();
			var childNamespaces = lookup[true].ToList();
			if(childNamespaces.Any())
			{
				var childNamespace = new NamespaceReference(childNamespaces.Cast<Namespace>());
				childSymbols.Add(childNamespace);
			}

			return childSymbols;
		}
	}
}
