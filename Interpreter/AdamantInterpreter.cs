using System.IO;
using Adamant.Exploratory.Compiler.Semantics;

namespace Adamant.Exploratory.Interpreter
{
	public class AdamantInterpreter
	{
		private readonly Package package;

		public AdamantInterpreter(Package package)
		{
			this.package = package;
		}

		public int Invoke(Function entryPoint, TextWriter consoleOut, TextWriter consoleError)
		{
			throw new System.NotImplementedException();
		}
	}
}
