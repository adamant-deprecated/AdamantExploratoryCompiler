using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// A Symbol is a semantic model for something that has a referenceable name.  This includes:
	///   * Declarations (Namespaces, Entities (Classes, Functions))
	///   * Members (Methods, Fields etc)
	///	  * Local Variable Declarations
	///   * Packages
	/// </summary>
	public abstract class Symbol : SemanticNode
	{
		private readonly IReadOnlyList<SyntaxNode> syntax;
		public Accessibility DeclaredAccessibility { get; }
		public string Name { get; }

		protected Symbol(SyntaxNode syntax, Accessibility declaredAccessibility, string name)
			: this(syntax.Yield(), declaredAccessibility, name)
		{
		}

		protected Symbol(IEnumerable<SyntaxNode> syntax, Accessibility declaredAccessibility, string name)
		{
			Requires.NotNull(name, nameof(name));

			this.syntax = syntax.ToList().AsReadOnly();
			Requires.That(this.syntax.All(s => s != null), nameof(syntax), "Syntax nodes must not be null");
			Name = name;
			DeclaredAccessibility = declaredAccessibility;
		}

		protected override IEnumerable<SyntaxNode> GetSyntax()
		{
			return syntax;
		}
	}
}
