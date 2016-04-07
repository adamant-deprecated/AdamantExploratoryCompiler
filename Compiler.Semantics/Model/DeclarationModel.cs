using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	internal abstract class DeclarationModel<TSyntax> : SymbolModel<TSyntax>, Declaration<TSyntax>
		where TSyntax : DeclarationSyntax
	{
		public override PackageModel ContainingPackage { get; }
		public readonly NamespaceModel ContainingNamespace;
		Namespace Declaration<TSyntax>.ContainingNamespace => ContainingNamespace;

		protected DeclarationModel(TSyntax syntax, PackageModel containingPackage, NamespaceModel containingNamespace, Accessibility declaredAccessibility, string name)
			: this(syntax.Yield(), containingPackage, containingNamespace, declaredAccessibility, name)
		{
		}

		protected DeclarationModel(IEnumerable<TSyntax> syntax, PackageModel containingPackage, NamespaceModel containingNamespace, Accessibility declaredAccessibility, string name)
			: base(syntax, declaredAccessibility, name)
		{
			Requires.NotNull(containingPackage, nameof(containingPackage));

			ContainingPackage = containingPackage;
			ContainingNamespace = containingNamespace;
		}

		public abstract IEnumerable<Declaration<DeclarationSyntax>> GetMembers();
		public abstract IEnumerable<Declaration<DeclarationSyntax>> GetMembers(string name);
	}
}
