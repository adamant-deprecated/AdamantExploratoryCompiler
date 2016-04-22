using System;

namespace Adamant.Exploratory.Forge.Commands
{
	public class CompileCommand : ProjectDirCommand
	{
		public CompileCommand()
		{
			IsCommand("compile", "Compile a forge project");
		}

		public override int Run(string[] remainingArguments)
		{
			try
			{
				var compiler = new ProjectCompiler(ProjectPath);
				var projects = compiler.Compile();
				return 0;
			}
			catch(CompileFailedException)
			{
				Console.WriteLine("Compile Failed, stopping");
				return 1;
			}
		}
	}
}
