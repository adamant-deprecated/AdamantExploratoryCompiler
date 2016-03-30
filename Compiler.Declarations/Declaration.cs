using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Declarations
{
	/// <summary>
	/// Declarations are used to handle the fact that namespaces and classes can be declared
	/// in multiple places in source and function names can be overloaded.
	/// </summary>
	public abstract class Declaration
	{
		public readonly string Name;

		protected Declaration(string name)
		{
			Requires.NotNullOrEmpty(name, nameof(name));
			Name = name;
		}
	}
}
