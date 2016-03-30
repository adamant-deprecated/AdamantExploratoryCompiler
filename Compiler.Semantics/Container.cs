using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// A container symbol is one that holds other declarations.  That is, it is
	/// either a namespace or a package.
	/// </summary>
	public interface Container
	{
		/// <summary>
		/// This contaier as a namespace.  null for packages
		/// </summary>
		Namespace AsNamespace { get; }

		PackageSyntax ContainingPackage { get; }

		IEnumerable<Declaration> GetMembers();
		IReadOnlyList<Declaration> GetMembers(string name);
	}
}
