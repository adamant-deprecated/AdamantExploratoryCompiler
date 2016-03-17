using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Symbols
{
	/// <summary>
	/// A container symbol is one that holds other declarations.  That is, it is
	/// either a namespace or a package.
	/// </summary>
	public interface ContainerSymbol
	{
		/// <summary>
		/// This contaier as a namespace.  null for packages
		/// </summary>
		NamespaceSymbol AsNamespace { get; }

		Package ContainingPackage { get; }

		IEnumerable<DeclarationSymbol> GetMembers();
		IReadOnlyList<DeclarationSymbol> GetMembers(string name);
	}
}
