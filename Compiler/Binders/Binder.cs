using Adamant.Exploratory.Compiler.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Binders
{
	/// <summary>
	/// A binder associates code references with their associated symbols
	/// </summary>
	public abstract class Binder
	{
		protected readonly Binder ContainingScope;

		protected Binder(Binder containingScope)
		{
			ContainingScope = containingScope;
		}

		public abstract LookupResult Lookup(Name name);

		public virtual LookupResult LookupInGlobalNamespace(Name name)
		{
			return ContainingScope.LookupInGlobalNamespace(name);
		}
	}
}
