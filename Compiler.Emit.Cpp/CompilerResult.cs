using System;

namespace Compiler.Emit.Cpp
{
	public class CompilerResult
	{
		public readonly int ExitCode;
		public readonly string Output;

		public CompilerResult(int exitCode, string output)
		{
			ExitCode = exitCode;
			Output = output;
		}

		public void WriteOutputToConsole()
		{
			Console.Write(Output);
		}
	}
}
