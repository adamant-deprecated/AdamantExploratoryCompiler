using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Ast;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public abstract class Builder<T> : RestrictedVisitor<T>
	{
		protected static IEnumerable<QualifiedName> GetNamespaces(AdamantParser.UsingStatementContext[] contexts)
		{
			return contexts.Select(s => new QualifiedName(s.namespaceName().GetText()));
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
	}
}
