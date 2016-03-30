using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// A package as a whole, forms the root of symbols.  It is also inherently, the global namespace.
	/// </summary>
	public class Package : Symbol, Container
	{
		private readonly MultiDictionary<string, Declaration> members = new MultiDictionary<string, Declaration>();

		public readonly string Name;
		// TODO Diagnostics
		public readonly PackageSyntax Syntax;
		Namespace Container.AsNamespace => null;

		public Package(PackageSyntax syntax, IEnumerable<Declaration> globalDeclarations)
			: base(null, Accessibility.NotApplicable, syntax?.Name)
		{
			Requires.NotNull(syntax, nameof(syntax));

			Name = syntax.Name;
			Syntax = syntax;
			foreach(var globalDeclaration in globalDeclarations)
				members.Add(globalDeclaration.Name, globalDeclaration);
		}

		protected override IReadOnlyList<Symbol> GetMembersInternal(string name)
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
