using System;

namespace Adamant.Exploratory.Forge.Commands
{
	public class InterpretCommand : ProjectDirCommand
	{
		public InterpretCommand()
		{
			IsCommand("interpret", "Run a forge project in the interpreter");
		}

		public override int Run(string[] remainingArguments)
		{
			try
			{
				var compiler = new ProjectCompiler(ProjectPath);
				var projects = compiler.Compile();
				// TODO run interpreter
				return 0;
			}
			catch(CompileFailedException)
			{
				Console.WriteLine("Build Failed, stopping");
				return 1;
			}
		}
	}
}
