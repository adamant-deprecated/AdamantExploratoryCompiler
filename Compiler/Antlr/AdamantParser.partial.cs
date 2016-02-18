using Antlr4.Runtime;

namespace Adamant.Exploratory.Compiler.Antlr
{
	public partial class AdamantParser
	{
		public AdamantParser(AdamantLexer lexer)
			: this(new CommonTokenStream(lexer))
		{
		}

		public AdamantParser(string fileName)
			: this(new AdamantLexer(fileName))
		{
		}
	}
}
