using System.Diagnostics;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax
{
	internal static class SyntaxRequires
	{
		[Conditional("DEBUG")]
		public static void TypeIs(SyntaxToken token, SyntaxTokenType type, string paramName)
		{
			if(token != null)
				Requires.That(token.Type == type, paramName, $"Token must be {type}");
		}
	}
}
