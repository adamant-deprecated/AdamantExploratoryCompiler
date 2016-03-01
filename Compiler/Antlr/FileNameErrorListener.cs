using System;
using Antlr4.Runtime;

namespace Adamant.Exploratory.Compiler.Antlr
{
	public class FileNameErrorListener : IAntlrErrorListener<IToken>
	{
		private readonly string fileName;
		private bool firstError = true;

		public FileNameErrorListener(string fileName)
		{
			this.fileName = fileName;
		}

		public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
		{
			if(!firstError) return;

			Console.WriteLine($"In '{fileName}'"); firstError = false;
		}
	}
}
