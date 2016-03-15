using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Directives;
using Adamant.Exploratory.Compiler.Syntax.Modifiers;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;
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

		protected static Accessibility GetAccessModifier(AdamantParser.ModifierContext[] modifiers)
		{
			// Return the first access modifier
			foreach(var modifier in modifiers)
				switch(modifier.token.Type)
				{
					case AdamantLexer.Public:
						return Accessibility.Public;
					case AdamantLexer.Protected:
						return Accessibility.Protected;
					case AdamantLexer.Package:
						return Accessibility.Package;
					case AdamantLexer.Private:
						return Accessibility.Private;
				}
			// If we don't find an acces modifier
			return Accessibility.Private;
		}

		protected static TextPosition PositionOf(IToken token)
		{
			return new TextPosition(token.StartIndex, token.Line, token.Column);
		}

		protected static Token Identifier(IToken token)
		{
			if(token == null) return null;
			Requires.EnumIn(token.Type, nameof(token), AdamantParser.Identifier, AdamantParser.EscapedIdentifier, AdamantParser.SizeType, AdamantParser.Self, AdamantParser.String);

			var text = token.Text;
			var valueText = text;
			if(token.Type == AdamantParser.EscapedIdentifier)
				valueText = text.Substring(1); // Remove the `
			return new Token(TokenType.Identifier, PositionOf(token), text, valueText);
		}

		protected static Token Identifier(AdamantParser.IdentifierContext context)
		{
			return Identifier(context?.token);
		}
	}
}
