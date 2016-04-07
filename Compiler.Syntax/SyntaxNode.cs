namespace Adamant.Exploratory.Compiler.Syntax
{
	/// <summary>
	/// A node of the syntax tree.
	///
	/// Note: A syntax node should be more like in Roslyn with child nodes.
	/// Unlike Roslyn I think it makes sense for trivia to be a separate type of node
	/// that is a child.  Generally nodes would contain any trivia between their first
	/// and last token.  However, classes and functions could contain their doc comments.
	/// </summary>
	public abstract class SyntaxNode
	{
	}
}
