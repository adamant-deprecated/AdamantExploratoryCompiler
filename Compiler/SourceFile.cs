using System.IO;
using Adamant.Exploratory.Compiler.Antlr;

namespace Adamant.Exploratory.Compiler
{
	public class SourceFile : SourceText
	{
		private readonly FileInfo fileInfo;

		public SourceFile(FileInfo fileInfo)
		{
			this.fileInfo = fileInfo;
		}

		public override string Name => fileInfo.FullName;

		public string Path => fileInfo.FullName;

		internal override AdamantParser NewParser()
		{
			return new AdamantParser(Path);
		}
	}
}
