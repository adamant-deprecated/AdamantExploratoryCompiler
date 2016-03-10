using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Directives;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;
using Adamant.Exploratory.Compiler.Syntax.Types;
using Antlr4.Runtime;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public abstract class Builder<T> : RestrictedVisitor<T>
	{
		protected static IEnumerable<UsingDirective> UsingDirective(AdamantParser.UsingDirectiveContext[] contexts)
		{
			return contexts.Select(s => new UsingDirective(s.namespaceName()
				._identifiers.Select(Identifier)
				.Aggregate(default(Name), (left, identifier) => left == null ? (Name)new IdentifierName(identifier) : new QualifiedName(left, new IdentifierName(identifier)))));
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

		protected static Token Identifier(IToken token)
		{
			Requires.EnumIn(token.Type, nameof(token), AdamantParser.Identifier, AdamantParser.EscapedIdentifier, AdamantParser.SizeType);

			var text = token.Text;
			var valueText = text;
			if(token.Type == AdamantParser.EscapedIdentifier)
				valueText = text.Substring(1); // Remove the `
			return new Token(TokenType.Identifier, PositionOf(token), text, valueText);
		}

		protected static Token Identifier(AdamantParser.IdentifierContext context)
		{
			return Identifier(context.name);
		}
	}
}
