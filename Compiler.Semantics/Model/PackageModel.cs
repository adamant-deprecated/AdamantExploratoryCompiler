using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	/// <summary>
	/// A package as a whole, forms the root of symbols.  It is also inherently, the global namespace.
	/// </summary>
	internal class PackageModel : SymbolModel<PackageSyntax>, Package
	{
		public override PackageModel ContainingPackage => this;
		public new PackageSyntax Syntax => base.Syntax.SingleOrDefault();
		public NamespaceModel GlobalNamespace { get; }
		Namespace Package.GlobalNamespace => GlobalNamespace;
		private readonly List<PackageReferenceModel> dependencies = new List<PackageReferenceModel>();
		public IReadOnlyList<PackageReferenceModel> Dependencies => dependencies;
		IEnumerable<PackageReference> Package.Dependencies => dependencies;
		public IReadOnlyList<Diagnostic> Diagnostics { get; private set; }
		private List<Entity<EntitySyntax>> entities;
		public IReadOnlyList<Entity<EntitySyntax>> Entities => entities;
		private List<FunctionModel> entryPoints;
		public IReadOnlyList<FunctionModel> EntryPoints => entryPoints;
		IEnumerable<Function> Package.EntryPoints => entryPoints;

		public PackageModel(PackageSyntax syntax)
			: base(syntax, Accessibility.NotApplicable, syntax?.Name)
		{
			GlobalNamespace = new NamespaceModel(this);
		}

		public void Add(IEnumerable<PackageReferenceModel> references)
		{
			foreach(var reference in references)
			{
				Requires.That(reference.ContainingPackage == this, nameof(reference), "Reference must be contained in package");
				dependencies.Add(reference);
			}
		}

		/// <summary>
		/// Populate the Entities property with all entities reachable from the GlobalNamespace
		/// </summary>
		public void FindEntities()
		{
			entities = new List<Entity<EntitySyntax>>();
			var namespaces = new Stack<NamespaceModel>();
			namespaces.Push(GlobalNamespace);
			while(namespaces.Count > 0)
			{
				var @namespace = namespaces.Pop();
				foreach(var declaration in @namespace.GetMembers())
					declaration.Match()
						.With<NamespaceModel>(ns => namespaces.Push(ns))
						.With<Entity<EntitySyntax>>(entity => entities.Add(entity))
						.Exhaustive();
			}
		}

		/// <summary>
		/// Populate the EntryPoints property with all Main functions
		/// </summary>
		public void FindEntryPoints()
		{
			entryPoints = entities.OfType<FunctionModel>()
				.Where(f => f.Name == "Main")
				.ToList();
		}

		public void Set(DiagnosticsBuilder diagnostics)
		{
			Diagnostics = diagnostics.Complete();
		}
	}
}
