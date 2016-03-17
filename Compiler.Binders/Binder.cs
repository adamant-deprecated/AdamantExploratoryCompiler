using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Binders.LookupResults;
using Adamant.Exploratory.Compiler.Syntax;
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

		public LookupResult Lookup(Name name, Package fromPackage)
		{
			return name.Match().Returning<LookupResult>()
				.With<QualifiedName>(qualifiedName =>
				{
					var context = Lookup(qualifiedName.Left, fromPackage);
					return context.Lookup(qualifiedName.Right, fromPackage);
				})
				.With<IdentifierName>(i=>Lookup(i, fromPackage))
				.Exhaustive();
		}

		protected abstract LookupResult Lookup(IdentifierName identifierName, Package fromPackage);

		public virtual LookupResult LookupInGlobalNamespace(Name name, Package fromPackage)
		{
			return ContainingScope.LookupInGlobalNamespace(name, fromPackage);
		}
	}
}
