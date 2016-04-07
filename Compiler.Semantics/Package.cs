using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// A package as a whole, forms the root of symbols.  It is also inherently, the global namespace.
	/// </summary>
	public class Package : Symbol
	{
		public override Package ContainingPackage => this;
		public new PackageSyntax Syntax => (PackageSyntax)base.Syntax.SingleOrDefault();
		public Namespace GlobalNamespace { get; }
		private readonly List<PackageReference> dependencies = new List<PackageReference>();
		public IReadOnlyList<PackageReference> Dependencies => dependencies;
		public IReadOnlyList<Diagnostic> Diagnostics { get; private set; }
		public IReadOnlyList<Entity> Entities { get; private set; }
		public IReadOnlyList<Function> EntryPoints { get; private set; }

		public Package(PackageSyntax syntax)
			: base(syntax, Accessibility.NotApplicable, syntax?.Name)
		{
			GlobalNamespace = new Namespace(this);
		}

		public void Add(IEnumerable<PackageReference> references)
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
			var entities = new List<Entity>();
			var namespaces = new Stack<Namespace>();
			namespaces.Push(GlobalNamespace);
			while(namespaces.Count > 0)
			{
				var @namespace = namespaces.Pop();
				foreach(var declaration in @namespace.GetMembers())
					declaration.Match()
						.With<Namespace>(ns => namespaces.Push(ns))
						.With<Entity>(entity => entities.Add(entity))
						.Exhaustive();
			}
			Entities = entities.AsReadOnly();
		}

		/// <summary>
		/// Populate the EntryPoints property with all Main functions
		/// </summary>
		public void FindEntryPoints()
		{
			EntryPoints = Entities.OfType<Function>()
				.Where(f => f.Name == "Main")
				.ToList()
				.AsReadOnly();
		}

		public void Set(DiagnosticsBuilder diagnostics)
		{
			Diagnostics = diagnostics.Complete();
		}
	}
}
