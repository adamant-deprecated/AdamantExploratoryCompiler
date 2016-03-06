using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class SymbolDefinitions : IReadOnlyList<SymbolDefinition>
	{
		public readonly Symbol Symbol;
		private readonly List<SymbolDefinition> definitions;

		public SymbolDefinitions(Symbol symbol, IEnumerable<SymbolDefinition> definitions)
		{
			Symbol = symbol;
			this.definitions = definitions.ToList();
		}

		public SymbolDefinitions(Symbol symbol, SymbolDefinition definition)
		{
			Symbol = symbol;
			definitions = new List<SymbolDefinition>() { definition };
		}

		public bool HasAccessibleDefinitions()
		{
			return definitions.Any(sd => sd.IsVisible);
		}

		public SymbolDefinitions Lookup(Symbol symbol)
		{
			var childDefinitions = definitions.Select(sd =>
			{
				var definition = sd.Definition.Definitions.TryGetValue(symbol);
				if(definition == null) return null;
				return new SymbolDefinition(definition, sd.InCurrentPackage);
			}).Where(sd => sd != null);
			return new SymbolDefinitions(symbol, childDefinitions);
		}

		public Definition Resolve()
		{
			var visibile = definitions.Where(sd => sd.IsVisible).ToList();
			if(visibile.Count == 1)
				return visibile.Single().Definition;

			var visibleInPackage = visibile.Where(sd => sd.InCurrentPackage).ToList();
			if(visibleInPackage.Count == 1)
				return visibleInPackage.Single().Definition; // TODO issue warning that we have chosen the one in the current package

			if(visibleInPackage.Count > 1 || visibile.Count > 1)
				throw new Exception($"Ambigous symbol {Symbol}");

			// Nothing visible in package
			if(definitions.Count > 0)
				throw new Exception($"Symbol is not accessible {Symbol}");

			throw new Exception("Symbol not defined");
		}

		#region IReadOnlyList<SymbolDefinition>
		public IEnumerator<SymbolDefinition> GetEnumerator()
		{
			return definitions.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count => definitions.Count;
		public SymbolDefinition this[int index] => definitions[index];
		#endregion
	}
}
