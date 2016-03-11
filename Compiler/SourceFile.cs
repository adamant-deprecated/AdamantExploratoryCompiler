using System;
using System.IO;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler
{
	public class SourceFile : ISourceFile
	{
		private readonly FileInfo fileInfo;

		public SourceFile(FileInfo fileInfo)
		{
			this.fileInfo = fileInfo;
		}

		public string Name => fileInfo.FullName;

		public string Path => fileInfo.FullName;

		public int CompareTo(ISourceFile other)
		{
			return string.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
