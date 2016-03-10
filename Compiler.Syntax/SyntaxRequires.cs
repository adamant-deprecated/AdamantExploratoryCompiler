using System.Diagnostics;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public static class SyntaxRequires
	{
		[Conditional("DEBUG")]
		public static void TypeIs(Token token, TokenType type, string paramName)
		{
			Requires.NotNull(token, paramName);
			Requires.That(token.Type == type,paramName, $"Token must be {type}");
		}
	}
}
