using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Antlr4.Runtime;

namespace Adamant.Exploratory.Compiler.Antlr
{
	public class GatherErrorsListener : IAntlrErrorListener<IToken>
	{
		private readonly DiagnosticsBuilder diagnostics;

		public GatherErrorsListener(DiagnosticsBuilder diagnostics)
		{
			this.diagnostics = diagnostics;
		}

		public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
		{
			// TODO we really should distinguish lexing and parsing errors
			diagnostics.ParseError(new TextPosition(0, line, charPositionInLine), msg);
		}
	}
}
