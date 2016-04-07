using System.Collections.Generic;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics
{
	/// <summary>
	/// A convenience base class.  This type represents a semantic node that corresponds with some particular
	/// element of the source syntax
	/// </summary>
	public class SourceSemanticNode : SemanticNode
	{
		public new SyntaxNode Syntax { get; }
		public override Package ContainingPackage { get; }

		public SourceSemanticNode(SyntaxNode syntax, Package containingPackage)
		{
			Requires.NotNull(syntax, nameof(syntax));
			Requires.NotNull(containingPackage, nameof(containingPackage));

			Syntax = syntax;
			ContainingPackage = containingPackage;
		}

		protected override IEnumerable<SyntaxNode> GetSyntax()
		{
			return Syntax.Yield();
		}
	}
}
