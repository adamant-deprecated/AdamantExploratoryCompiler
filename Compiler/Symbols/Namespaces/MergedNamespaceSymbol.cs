using System.Collections.Generic;

namespace Adamant.Exploratory.Compiler.Symbols.Namespaces
{
	/// <summary>
	/// Represents the namespace as declared across multiple packages.  It merges the
	/// package namespaces together
	/// </summary>
	public class MergedNamespaceSymbol : NamespaceSymbol
	{
		// TODO create a correct constructor
		public MergedNamespaceSymbol()
			: base(null, null, null)
		{
		}

		public override IReadOnlyList<DeclarationSymbol> GetMembers(string name)
		{
			throw new System.NotImplementedException();
		}
	}
}
