using System.IO;
using Adamant.Exploratory.Compiler.Antlr;
using Antlr4.Runtime;

namespace Adamant.Exploratory.Compiler
{
	public class SourceReader : SourceText
	{
		private readonly TextReader reader;

		public SourceReader(string name, TextReader reader)
		{
			this.reader = reader;
			Name = name;
		}

		public override string Name { get; }
		internal override AdamantParser NewParser()
		{
			return new AdamantParser(new AdamantLexer(new AntlrInputStream(reader)));
		}
	}
}
