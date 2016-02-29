using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public class Definitions : KeyedCollection<Symbol, Definition>
	{
		public Definitions()
		{
		}

		public Definitions(IEnumerable<Definition> definitions)
		{
			foreach(var definition in definitions)
				Add(definition);
		}

		protected override Symbol GetKeyForItem(Definition definition)
		{
			return definition.Name;
		}

		public bool TryGetValue(Symbol symbol, out Definition definition)
		{
			if(Dictionary != null) return Dictionary.TryGetValue(symbol, out definition);
			definition = default(Definition);
			return false;
		}

		public new bool Contains(Definition definition)
		{
			return Contains(GetKeyForItem(definition));
		}

		public Definition TryGetValue(Symbol symbol)
		{
			Definition definition;
			// If no keys have been added, there will be no dictionary
			return Dictionary == null || !Dictionary.TryGetValue(symbol, out definition) ? null : definition;
		}
	}
}
