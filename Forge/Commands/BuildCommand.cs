using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Adamant.Exploratory.Compiler;
using Adamant.Exploratory.Forge.Config;
using Newtonsoft.Json;
using Project = Adamant.Exploratory.Compiler.Syntax.Project;

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
				var projects = new Dictionary<string, Project>();
				var compiler = new AdamantCompiler();
				Forge(ProjectPath, projects, compiler);
				return 0;
			}
			catch(BuildFailedException)
			{
				Console.WriteLine("Build Failed, stopping");
				return 1;
			}
		}

		private static void Forge(string projectFilePath, IDictionary<string, Project> projects, AdamantCompiler compiler)
		{
			var projectDirPath = Path.GetFullPath(Path.GetDirectoryName(projectFilePath));
			var projectConfig = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText(projectFilePath));
			projectConfig.Name = projectConfig.Name ?? Path.GetFileName(projectDirPath);

			BuildDependencies(projectDirPath, projectConfig, projects, compiler);
			var targetDirPath = BuildProject(projectDirPath, projectConfig, projects, compiler);
			BuildProjects(projectDirPath, projectConfig, projects, targetDirPath, compiler);
		}

		private static void BuildDependencies(string projectDirPath, ProjectConfig projectConfig, IDictionary<string, Project> projects, AdamantCompiler compiler)
		{
			foreach(var dependency in projectConfig.Dependencies)
			{
				var dependencyName = dependency.Key;
				if(projects.ContainsKey(dependencyName)) continue;
				var path = DependencyPath(dependencyName, dependency.Value, projectDirPath, projectConfig.DependencyPaths);
				Forge(Path.Combine(path, ProjectFile.Name), projects, compiler);
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

		private static string BuildProject(string projectDirPath, ProjectConfig projectConfig, IDictionary<string, Project> projects, AdamantCompiler compiler)
		{
			Console.WriteLine($"Building {projectConfig.Name} ...");
			var compileDirPath = Path.Combine(projectDirPath, ".forge-cache");
			DeleteDirectoryIfExists(compileDirPath);

			var sourceFiles = new DirectoryInfo(Path.Combine(projectDirPath, "src")).GetFiles("*.adam", SearchOption.AllDirectories);

			var dependencies = projectConfig.Dependencies.Select(dependency => projects[dependency.Key]);
			var project = compiler.CompileProject(sourceFiles.Select(sourceFile => compiler.Parse(sourceFile.FullName)), dependencies);

			var isApp = projectConfig.Template == "app";
			var targetDirPath = Path.Combine(projectDirPath, "targets/debug");
			DeleteDirectoryIfExists(targetDirPath);

			if(isApp)
			{
				Directory.CreateDirectory(compileDirPath);
				// TODO emit cpp and compile it
			}
			projects.Add(projectConfig.Name, project);
			return targetDirPath;
		}

		private static void BuildProjects(
			string projectDirPath,
			ProjectConfig projectConfig,
			IDictionary<string, Project> projects,
			string targetDirPath,
			AdamantCompiler compiler)
		{
			// Build Projects that weren't already built as projects
			foreach(var project in projectConfig.Projects)
			{
				var projectName = project.Key;
				if(projects.ContainsKey(projectName)) continue;
				Forge(Path.Combine(projectDirPath, project.Value, ProjectFile.Name), projects, compiler);
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
