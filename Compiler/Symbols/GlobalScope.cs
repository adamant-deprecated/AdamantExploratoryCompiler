using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class GlobalScope : NameScope
	{
		private readonly Definitions globalDefinitions;
		private readonly List<Package> dependencies;

		public GlobalScope(Definitions globalDefinitions, IEnumerable<Package> dependencies)
		{
			this.globalDefinitions = globalDefinitions;
			this.dependencies = dependencies.ToList();
		}

		public Definitions Definitions => globalDefinitions;

		public override GlobalScope Globals => this;

		public override Definition LookupLocal(Symbol name)
		{
			return globalDefinitions.TryGetValue(name);
		}

		public override SymbolDefinitions Lookup(Symbol name)
		{
			var definition = globalDefinitions.TryGetValue(name);
			var currentPackage = definition != null
									? new[] { new SymbolDefinition(definition, true) }
									: Enumerable.Empty<SymbolDefinition>();
			var otherPackages = dependencies.Select(p => p.GlobalDefinitions.TryGetValue(name))
				.Where(d => d != null)
				.Select(d => new SymbolDefinition(d, false));

			return new SymbolDefinitions(name, currentPackage.Concat(otherPackages));
		}

		public SymbolDefinitions LookupInPackage(Symbol name, string packageName)
		{
			var package = dependencies.Single(p => p.Name == packageName);
			var definition = package.GlobalDefinitions.TryGetValue(name);
			if(definition != null)
				return new SymbolDefinitions(name, new SymbolDefinition(definition, false));

			return new SymbolDefinitions(name, Enumerable.Empty<SymbolDefinition>());
		}
	}
}
