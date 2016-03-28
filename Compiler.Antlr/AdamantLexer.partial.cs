using System.Collections.Generic;
using System.Text;
using Adamant.Exploratory.Compiler.Antlr.Preprocessor;
using Antlr4.Runtime;

namespace Adamant.Exploratory.Compiler.Antlr
{
	public partial class AdamantLexer
	{
		public static IReadOnlyDictionary<int, string> ChannelNames { get; } = new Dictionary<int, string>()
		{
			{ 0, "Default" }, { 2, "DocComments" }
		};
		public static readonly Encoding Encoding = new UTF8Encoding(false, true);

		private readonly PreprocessorState preprocessorState = new PreprocessorState();
		private PreprocessorVisitor preprocessorVisitor;

		public AdamantLexer(string fileName)
			: this(new AntlrFileStream(fileName, Encoding))
		{
		}

		public PreprocessorState PreprocessorState => preprocessorState;

		private void Preprocess()
		{
			if(TokenStartColumn != 0)
				NotifyErrorListeners("Preprocessor directives must be the first non-whitespace character on the line");

			var directive = Text;
			var stream = new AntlrInputStream(directive);
			var lexer = new PreprocessorLineLexer(stream);
			var tokens = new CommonTokenStream(lexer);
			var parser = new PreprocessorLineParser(tokens) { BuildParseTree = true };
			var tree = parser.preprocessorLine();
			if(preprocessorVisitor == null) preprocessorVisitor = new PreprocessorVisitor(preprocessorState); //annoyingly can't use constructor
			tree.Accept(preprocessorVisitor);
			CurrentMode = preprocessorState.InSkippedSection ? PREPROCESSOR_SKIP : DefaultMode;
		}

		public void NotifyErrorListeners(string msg)
		{
			NotifyErrorListeners(msg, null);
		}

		public virtual void NotifyErrorListeners(string msg, RecognitionException e)
		{
			int line = TokenStartLine;
			int charPositionInLine = TokenStartColumn;
			var antlrErrorListener = ErrorListenerDispatch;
			antlrErrorListener.SyntaxError(this, Type, line, charPositionInLine, msg, e);
		}
	}
}
