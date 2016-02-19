using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Adamant.Exploratory.Compiler;
using Adamant.Exploratory.Forge.Config;
using Newtonsoft.Json;

namespace Adamant.Exploratory.Forge.Commands
{
	public class BuildCommand : ProjectDirCommand
	{
		public BuildCommand()
		{
			IsCommand("build", "Build a forge project");
		}

		public override int Run(string[] remainingArguments)
		{
			try
			{
				var builtDependencies = new Dictionary<string, Dependency>();
				var compiler = new AdamantCompiler();
				Forge(ProjectPath, builtDependencies, compiler);
				return 0;
			}
			catch(BuildFailedException)
			{
				Console.WriteLine("Build Failed, stopping");
				return 1;
			}
		}

		private static readonly string[] CoreDependencies = { "Adamant.Compiler.Runtime", "Adamant.Compiler.System" };

		private static void Forge(string projectFilePath, IDictionary<string, Dependency> builtDependencies, AdamantCompiler compiler)
		{
			var projectDirPath = Path.GetFullPath(Path.GetDirectoryName(projectFilePath));
			var projectConfig = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText(projectFilePath));
			projectConfig.Name = projectConfig.Name ?? Path.GetFileName(projectDirPath);

			BuildDependencies(projectDirPath, projectConfig, builtDependencies, compiler);
			var targetDirPath = BuildProject(projectDirPath, projectConfig, builtDependencies, compiler);
			BuildProjects(projectDirPath, projectConfig, builtDependencies, targetDirPath, compiler);
		}

		private static void BuildDependencies(string projectDirPath, ProjectConfig projectConfig, IDictionary<string, Dependency> builtDependencies, AdamantCompiler compiler)
		{
			foreach(var dependency in projectConfig.Dependencies)
			{
				var dependencyName = dependency.Key;
				if(builtDependencies.ContainsKey(dependencyName)) continue;
				var path = DependencyPath(dependencyName, dependency.Value, projectDirPath, projectConfig.DependencyPaths);
				Forge(Path.Combine(path, Project.FileName), builtDependencies, compiler);
			}
		}

		private static string DependencyPath(string dependencyName, DependencyConfig config, string projectDirPath, IList<string> dependencyPaths)
		{
			if(!string.IsNullOrEmpty(config.Path))
			{
				return Path.Combine(projectDirPath, config.Path);
			}

			foreach(var dependencyPath in dependencyPaths)
			{
				var tryPath = Path.Combine(projectDirPath, dependencyPath, dependencyName);
				if(Directory.Exists(tryPath))
					return tryPath;
			}
			throw new Exception("Could not find dependency");
		}

		private static string BuildProject(string projectDirPath, ProjectConfig projectConfig, IDictionary<string, Dependency> builtDependencies, AdamantCompiler compiler)
		{
			Console.WriteLine($"Building {projectConfig.Name} ...");
			var compileDirPath = Path.Combine(projectDirPath, ".forge-cache");
			DeleteDirectoryIfExists(compileDirPath);

			var sourceFiles = new DirectoryInfo(Path.Combine(projectDirPath, "src")).GetFiles("*.adam", SearchOption.AllDirectories);

			var assemblage = compiler.Combine(sourceFiles.Select(sourceFile => compiler.Parse(sourceFile.FullName)));

			var isApp = projectConfig.Template == "app";
			var targetDirPath = Path.Combine(projectDirPath, "targets/debug");
			DeleteDirectoryIfExists(targetDirPath);
			var dependencies = projectConfig.Dependencies.Keys.Concat(CoreDependencies).ToList();
			if(isApp)
			{
				Directory.CreateDirectory(compileDirPath);
				// TODO emit cpp and compile it
			}
			builtDependencies.Add(projectConfig.Name, new Dependency(assemblage, dependencies));
			return targetDirPath;
		}

		private static void BuildProjects(
			string projectDirPath,
			ProjectConfig projectConfig,
			IDictionary<string, Dependency> builtDependencies,
			string targetDirPath,
			AdamantCompiler compiler)
		{
			// Build Projects that weren't already built as dependencies
			foreach(var project in projectConfig.Projects)
			{
				var projectName = project.Key;
				if(builtDependencies.ContainsKey(projectName)) continue;
				Forge(Path.Combine(projectDirPath, project.Value, Project.FileName), builtDependencies, compiler);
				// TODO copy into target
			}
		}

		private static void DeleteDirectoryIfExists(string path)
		{
			for(var i = 0; i < 3; i++)
			{
				try
				{
					if(Directory.Exists(path))
					{
						Directory.Delete(path, true);
						// Having problems with creating dir immediately after deleting
						while(Directory.Exists(path))
							Thread.Sleep(1);
					}
					return; // if no error, don't return
				}
				catch(IOException)
				{
					// Ignore, we want to retry
				}
			}
		}
	}
}
