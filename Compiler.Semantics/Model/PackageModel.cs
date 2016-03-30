using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax;
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
		private List<FunctionModel> entryPoints;
		public List<FunctionModel> EntryPoints => entryPoints;
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

		public void FindEntryPoints()
		{
			entryPoints = new List<FunctionModel>();
			var containers = new Stack<NamespaceModel>();
			containers.Push(GlobalNamespace);
			while(containers.Count > 0)
			{
				var container = containers.Pop();
				foreach(var symbol in container.GetMembers())
					symbol.Match()
						.With<NamespaceModel>(@namespace => containers.Push(@namespace))
						.With<FunctionModel>(function => { if(function.Name == "Main") entryPoints.Add(function); })
						.Ignore<ClassModel>()
						.Exhaustive();
			}
		}

		public void Set(DiagnosticsBuilder diagnostics)
		{
			Diagnostics = diagnostics.Complete();
		}
	}
}
