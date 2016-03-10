using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Antlr4.Runtime;

namespace Adamant.Exploratory.Compiler.Antlr
{
	public class GatherErrorsListener : IAntlrErrorListener<IToken>
	{
		private readonly List<Diagnostic> errors = new List<Diagnostic>();
		private readonly ISourceFile sourceFile;

		public GatherErrorsListener(ISourceFile sourceFile)
		{
			this.sourceFile = sourceFile;
		}

		public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
		{
			// TODO we really should distinguish lexing and parsing errors
			errors.Add(Diagnostic.ParseError(sourceFile, new TextPosition(0, line, charPositionInLine), msg));
		}

		public IEnumerable<Diagnostic> Errors => errors;
	}
}
