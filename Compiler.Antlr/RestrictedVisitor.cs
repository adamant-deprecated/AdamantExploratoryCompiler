using System;
using Antlr4.Runtime.Tree;

namespace Adamant.Exploratory.Compiler.Antlr
{
	/// <summary>
	/// A visitor that is restricted to visiting specific node types, any node visited that is not
	/// overridden in a sub-class will throw NotSupportedException.
	/// </summary>
	public abstract class RestrictedVisitor<T> : AdamantParserBaseVisitor<T>
	{
		public override T Visit(IParseTree tree)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override T VisitChildren(IRuleNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override T VisitTerminal(ITerminalNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}

		public override T VisitErrorNode(IErrorNode node)
		{
			throw new NotSupportedException("Generic visit methods should not be called.");
		}
	}
}
