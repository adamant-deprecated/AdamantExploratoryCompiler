using System.Collections.Generic;

namespace Adamant.Exploratory.Compiler.Semantics.References
{
	/// <summary>
	/// A symbol reference referes to a symbol across a compilation
	/// </summary>
	internal abstract class DeclarationReference
	{
		public abstract string Name { get; }
		public abstract bool IsIn(Package package);
		public abstract bool IsVisibleFrom(Package package);
		public abstract IEnumerable<DeclarationReference> GetMembers(string name);
	}
}
