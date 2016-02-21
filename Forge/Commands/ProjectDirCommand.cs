using System.IO;
using ManyConsole;

namespace Adamant.Exploratory.Forge.Commands
{
	public abstract class ProjectDirCommand : ConsoleCommand
	{
		protected ProjectDirCommand()
		{
			AllowsAnyAdditionalArguments("<project directory>");
		}

		public override int? OverrideAfterHandlingArgumentsBeforeRun(string[] remainingArguments)
		{
			if(remainingArguments.Length > 1)
				VerifyNumberOfArguments(remainingArguments, 1);

			var projectDirPath = remainingArguments.Length > 0 ? remainingArguments[0] : ".";

			var projectPath = Path.GetFullPath(Path.Combine(projectDirPath, ProjectFile.Name));
			if(!File.Exists(projectPath))
				throw new ConsoleHelpAsException("No project found in directory");

			ProjectPath = projectPath;

			return null;
		}

		protected string ProjectPath { get; set; }
	}
}
