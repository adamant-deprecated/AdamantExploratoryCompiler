using System;
using ManyConsole;

namespace Adamant.Exploratory.Forge
{
	public class Program
	{
		private static int Main(string[] args)
		{
			var commands = ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
			return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
		}
	}
}
