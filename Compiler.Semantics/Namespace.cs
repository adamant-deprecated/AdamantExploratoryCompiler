using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// Represents a namespace as it is declared within a package
	/// </summary>
	public class Namespace : Declaration
	{
		private readonly MultiDictionary<string, Declaration> members = new MultiDictionary<string, Declaration>();
		public new IEnumerable<NamespaceSyntax> Syntax => base.Syntax.Cast<NamespaceSyntax>();

		public Namespace(Package containingPackage)
			: base(Enumerable.Empty<NamespaceSyntax>(), containingPackage, null, Accessibility.Public, "")
		{
		}

		public Namespace(IEnumerable<NamespaceSyntax> syntax, Namespace containingNamespace, string name)
			: base(syntax, containingNamespace.ContainingPackage, containingNamespace, Accessibility.Public, name) // namespaces are implicitly public
		{
		}

		public override IEnumerable<Declaration> GetMembers()
		{
			return members.Values;
		}

		public override IEnumerable<Declaration> GetMembers(string name)
		{
			return members[name];
		}

		public void Add(Declaration child)
		{
			Requires.That(child.ContainingNamespace == this, nameof(child), "Child must be contained in this namespace");
			members.Add(child.Name, child);
		}
	}
}
