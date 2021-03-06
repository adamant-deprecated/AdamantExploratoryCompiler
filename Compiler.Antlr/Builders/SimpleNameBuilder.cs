﻿using System.ComponentModel;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class SimpleNameBuilder : Builder<SimpleNameSyntax>
	{
		public override SimpleNameSyntax VisitIdentifierName(AdamantParser.IdentifierNameContext context)
		{
			var name = Identifier(context.identifierOrPredefinedType());
			switch(name.Type)
			{
				case SyntaxTokenType.Identifier:
					return new IdentifierNameSyntax(name);
				case SyntaxTokenType.PredefinedType:
					return new PredefinedTypeSyntax(name);
				default:
					throw new InvalidEnumArgumentException("Unsupported TokenType", (int)name.Type, typeof(SyntaxTokenType));
			}
		}

		public override SimpleNameSyntax VisitGenericName(AdamantParser.GenericNameContext context)
		{
			return new GenericNameSyntax(Identifier(context.identifierOrPredefinedType()));
		}
	}
}
