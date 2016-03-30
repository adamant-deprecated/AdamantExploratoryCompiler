using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	/// <summary>
	/// Represents a namespace as it is declared within a package
	/// </summary>
	internal class NamespaceModel : DeclarationModel<NamespaceSyntax>, Namespace
	{
		private readonly MultiDictionary<string, Declaration<DeclarationSyntax>> members = new MultiDictionary<string, Declaration<DeclarationSyntax>>();

		public NamespaceModel(PackageModel containingPackage)
			: base(Enumerable.Empty<NamespaceSyntax>(), containingPackage, null, Accessibility.Public, "")
		{
		}

		public NamespaceModel(IEnumerable<NamespaceSyntax> syntax, NamespaceModel containingNamespace, string name)
			: base(syntax, containingNamespace.ContainingPackage, containingNamespace, Accessibility.Public, name) // namespaces are implicitly public
		{
		}

		//protected  IReadOnlyList<SemanticModel> GetMembersInternal(string name)
		//{
		//	return members[name];
		//}

		public override IEnumerable<Declaration<DeclarationSyntax>> GetMembers()
		{
			return members.Values;
		}

		public override IEnumerable<Declaration<DeclarationSyntax>> GetMembers(string name)
		{
			return members[name];
		}

		public void Add(Declaration<DeclarationSyntax> child)
		{
			Requires.That(child.ContainingNamespace == this, nameof(child), "Child must be contained in this namespace");
			members.Add(child.Name, child);
		}
	}
}
