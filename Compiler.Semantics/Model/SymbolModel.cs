using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	/// <summary>
	/// A Symbol is a semantic model for something that has a referenceable name.  This includes:
	///   * Declarations (Namespaces, Entities (Classes, Functions))
	///   * Members (Methods, Fields etc)
	///	  * Local Variable Declarations
	///   * Packages
	/// </summary>
	internal abstract class SymbolModel<TSyntax> : SemanticModel<TSyntax>, Symbol<TSyntax>
		where TSyntax : SyntaxNode
	{
		public Accessibility DeclaredAccessibility { get; }
		public string Name { get; }

		protected SymbolModel(TSyntax syntax, Accessibility declaredAccessibility, string name)
			: base(syntax)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
			DeclaredAccessibility = declaredAccessibility;
		}

		protected SymbolModel(IEnumerable<TSyntax> syntax, Accessibility declaredAccessibility, string name)
			: base(syntax)
		{
			Requires.NotNull(name, nameof(name));

			Name = name;
			DeclaredAccessibility = declaredAccessibility;
		}
	}
}
