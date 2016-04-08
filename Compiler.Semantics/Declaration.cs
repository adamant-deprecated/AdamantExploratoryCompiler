using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public abstract class Declaration : Symbol
	{
		public override Package ContainingPackage { get; }
		public readonly Namespace ContainingNamespace;
		public new IEnumerable<DeclarationSyntax> Syntax => base.Syntax.Cast<DeclarationSyntax>();

		protected Declaration(DeclarationSyntax syntax, Package containingPackage, Namespace containingNamespace, Accessibility declaredAccessibility, string name)
			: this(syntax.Yield(), containingPackage, containingNamespace, declaredAccessibility, name)
		{
		}

		protected Declaration(IEnumerable<DeclarationSyntax> syntax, Package containingPackage, Namespace containingNamespace, Accessibility declaredAccessibility, string name)
			: base(syntax, declaredAccessibility, name)
		{
			Requires.NotNull(containingPackage, nameof(containingPackage));

			ContainingPackage = containingPackage;
			ContainingNamespace = containingNamespace;
		}

		public abstract IEnumerable<Declaration> GetMembers();
		public abstract IEnumerable<Declaration> GetMembers(string name);

		public virtual IEnumerable<string> QualifiedName()
		{
			if(ContainingNamespace == null)
				return Name.Yield();

			return ContainingNamespace.QualifiedName().Append(Name);
		}
	}
}
