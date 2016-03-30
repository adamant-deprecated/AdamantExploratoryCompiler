using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// Represents a namespace as it is declared within a package
	/// </summary>
	public class Namespace : Declaration, Container
	{
		private readonly MultiDictionary<string, Declaration> members = new MultiDictionary<string, Declaration>();

		Namespace Container.AsNamespace => this;

		public Namespace(PackageSyntax containingPackage, string name, IEnumerable<Declaration> declarations)
			: base(containingPackage, Accessibility.Public, name) // namespaces are implicitly public
		{
			foreach(var declaration in declarations)
				members.Add(declaration.Name, declaration);
		}

		protected override IReadOnlyList<SemanticModel> GetMembersInternal(string name)
		{
			return members[name];
		}

		public IEnumerable<Declaration> GetMembers()
		{
			return members.Values;
		}

		public new IReadOnlyList<Declaration> GetMembers(string name)
		{
			return members[name];
		}
	}
}
