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
		protected static IEnumerable<UsingSyntax> UsingDirective(AdamantParser.UsingDirectiveContext[] contexts)
		{
			return contexts.Select(s => new UsingSyntax(s.namespaceName()
				._identifiers.Select(Identifier)
				.Aggregate(default(NameSyntax), (left, identifier) => left == null ? (NameSyntax)new IdentifierNameSyntax(identifier) : new QualifiedNameSyntax(left, new IdentifierNameSyntax(identifier)))));
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

		private static readonly int[] identifierTokenTypes =
		{
			AdamantParser.Identifier, AdamantParser.EscapedIdentifier,
			AdamantParser.String,
			AdamantParser.ByteType,
			AdamantParser.IntType,
			AdamantParser.UIntType,
			AdamantParser.FloatType,
			AdamantParser.FixedType,
			AdamantParser.DecimalType,
			AdamantParser.SizeType,
			AdamantParser.OffsetType,
			AdamantParser.Self,
		};

		protected static SyntaxToken Identifier(IToken token)
		{
			if(token == null) return null;
			Requires.EnumIn(token.Type, nameof(token), identifierTokenTypes);

			var tokenType = SyntaxTokenType.Identifier;
			var text = token.Text;
			var valueText = text;
			if(token.Type == AdamantParser.EscapedIdentifier)
				valueText = text.Substring(1); // Remove the `
			else if(token.Type != AdamantParser.Identifier)
			{
				// Unsafe array is handled more like a regular identifier becuase it has generic type params
				tokenType = token.Type == AdamantParser.UnsafeArrayType ? SyntaxTokenType.Identifier : SyntaxTokenType.PredefinedType;
				valueText = "#" + valueText; // Special identifiers like predefined type we distiguish by prefixing with a special char
			}
			return new SyntaxToken(tokenType, PositionOf(token), text, valueText);
		}

		protected static SyntaxToken Identifier(AdamantParser.IdentifierContext context)
		{
			return Identifier(context?.token);
		}

		protected static SyntaxToken Identifier(AdamantParser.IdentifierOrPredefinedTypeContext context)
		{
			return context.token != null ? Identifier(context.token) : Identifier(context.identifier());
		}
	}
}
