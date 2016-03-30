using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Symbols
{
	/// <summary>
	/// A package as a whole, forms the root of symbols.  It is also inherently, the global namespace.
	/// </summary>
	public class PackageSymbol : Symbol, ContainerSymbol
	{
		private readonly MultiDictionary<string, DeclarationSymbol> members = new MultiDictionary<string, DeclarationSymbol>();

		public readonly PackageSyntax PackageSyntax;
		NamespaceSymbol ContainerSymbol.AsNamespace => null;

		public PackageSymbol(PackageSyntax packageSyntax, IEnumerable<DeclarationSymbol> globalDeclarations)
			: base(null, Accessibility.NotApplicable, packageSyntax?.Name)
		{
			Requires.NotNull(packageSyntax, nameof(packageSyntax));

			PackageSyntax = packageSyntax;
			foreach(var globalDeclaration in globalDeclarations)
				members.Add(globalDeclaration.Name, globalDeclaration);
		}

		protected override IReadOnlyList<Symbol> GetMembersInternal(string name)
		{
			return members[name];
		}

		public IEnumerable<DeclarationSymbol> GetMembers()
		{
			return members.Values;
		} 

		public new IReadOnlyList<DeclarationSymbol> GetMembers(string name)
		{
			return members[name];
		}
	}
}
