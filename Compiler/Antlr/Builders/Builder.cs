using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations;
using Antlr4.Runtime;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public abstract class Builder<T> : RestrictedVisitor<T>
	{
		protected static IEnumerable<UsingStatement> UsingStatements(AdamantParser.UsingStatementContext[] contexts)
		{
			return contexts.Select(s => new UsingStatement(PositionOf(s.namespaceName().Start),
					s.namespaceName()
						._identifiers.Select(Symbol)
						.Aggregate(default(FullyQualifiedName), (name, symbol) => name.Append(symbol))));
		}

		protected static AccessModifier GetAccessModifier(AdamantParser.ModifierContext[] modifiers)
		{
			// Return the first access modifier
			foreach(var modifier in modifiers)
				switch(modifier.Symbol.Type)
				{
					case AdamantLexer.Public:
						return AccessModifier.Public;
					case AdamantLexer.Protected:
						return AccessModifier.Protected;
					case AdamantLexer.Package:
						return AccessModifier.Package;
					case AdamantLexer.Private:
						return AccessModifier.Private;
				}
			// If we don't find an acces modifier
			return AccessModifier.Private;
		}

		protected static TextPosition PositionOf(IToken token)
		{
			return new TextPosition(token.StartIndex, token.Line, token.Column);
		}

		protected static Symbol Symbol(IToken token)
		{
			return new Symbol(token.Text);
		}

		protected static Symbol Symbol(AdamantParser.IdentifierContext context)
		{
			return new Symbol(context.GetText());
		}
	}
}
