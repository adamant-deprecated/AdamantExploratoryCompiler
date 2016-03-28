using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Adamant.Exploratory.Compiler;
using Adamant.Exploratory.Compiler.Compiled;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.PackageConfig;
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
				var packages = new Dictionary<string, CompiledPackage>();
				var compiler = new AdamantCompiler();
				Forge(ProjectPath, packages, compiler);
				return 0;
			}
			catch(BuildFailedException)
			{
				Console.WriteLine("Build Failed, stopping");
				return 1;
			}
		}

		private static void Forge(string projectFilePath, IDictionary<string, CompiledPackage> packages, AdamantCompiler compiler)
		{
			var projectDirPath = Path.GetFullPath(Path.GetDirectoryName(projectFilePath));
			var projectConfig = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText(projectFilePath));
			projectConfig.Name = projectConfig.Name ?? Path.GetFileName(projectDirPath);

			// TODO this should be controlled by the compiler plugin so that the version matches
			if(projectConfig.Name != "System.Runtime")
				projectConfig.Dependencies.Add("System.Runtime", new DependencyConfig() { Version = "*" });

			BuildDependencies(projectDirPath, projectConfig, packages, compiler);
			var targetDirPath = BuildProject(projectDirPath, projectConfig, packages, compiler);
			BuildProjects(projectDirPath, projectConfig, packages, targetDirPath, compiler);
		}

		private static void BuildDependencies(string projectDirPath, ProjectConfig projectConfig, IDictionary<string, CompiledPackage> packages, AdamantCompiler compiler)
		{
			foreach(var dependency in projectConfig.Dependencies)
			{
				var dependencyName = dependency.Key;
				if(packages.ContainsKey(dependencyName)) continue;
				var path = DependencyPath(dependencyName, dependency.Value, projectDirPath, projectConfig.DependencyPaths);
				Forge(Path.Combine(path, ProjectFile.Name), packages, compiler);
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

		private static string BuildProject(string projectDirPath, ProjectConfig projectConfig, IDictionary<string, CompiledPackage> packages, AdamantCompiler compiler)
		{
			Console.WriteLine($"Building {projectConfig.Name} ...");
			var compileDirPath = Path.Combine(projectDirPath, ".forge-cache");
			DeleteDirectoryIfExists(compileDirPath);

			var isApp = projectConfig.Template == "app";
			var targetDirPath = Path.Combine(projectDirPath, "targets/debug");

			var sourceFiles = new DirectoryInfo(Path.Combine(projectDirPath, "src")).GetFiles("*.adam", SearchOption.AllDirectories);
			// TODO read trusted from config
			var package = new Package(projectConfig.Name, projectConfig.Dependencies.Select(d => new PackageDependency(d.Key, null, true)));
			package = package.With(sourceFiles.Select(fileInfo => compiler.Parse(package, new SourceFile(fileInfo))));
			if(package.Diagnostics.Count > 0)
			{
				PrintDiagnostics(package);
				return targetDirPath;
			}
			var compiledPackage = compiler.Compile(package, packages.Values);

			DeleteDirectoryIfExists(targetDirPath);

			Directory.CreateDirectory(compileDirPath);
			var cppSource = compiler.EmitCpp(compiledPackage);
			using(var file = File.CreateText(Path.Combine(compileDirPath, compiledPackage.Name + ".cpp")))
			{
				file.Write(cppSource);
			}
			// TODO Copy forward cpp files from dependencies

			packages.Add(compiledPackage.Name, compiledPackage);
			return targetDirPath;
		}

		private static void BuildProjects(
			string projectDirPath,
			ProjectConfig projectConfig,
			IDictionary<string, CompiledPackage> packages,
			string targetDirPath,
			AdamantCompiler compiler)
		{
			// Build Projects that weren't already built as dependencies
			foreach(var project in projectConfig.Projects)
			{
				var projectName = project.Key;
				if(packages.ContainsKey(projectName)) continue;
				Forge(Path.Combine(projectDirPath, project.Value, ProjectFile.Name), packages, compiler);
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

		private static void PrintDiagnostics(Package package)
		{
			ISourceText file = null;
			foreach(var diagnostic in package.Diagnostics)
			{
				if(file != diagnostic.File)
				{
					file = diagnostic.File;
					Console.WriteLine($"In {file.Name}");
				}
				var level = diagnostic.Level.ToString();
				var line = diagnostic.Position.Line + 1;
				var column = diagnostic.Position.Column + 1;
				Console.WriteLine($"{level} on line {line} at character {column}: ");
				Console.WriteLine($"    {diagnostic.Message}");
			}
		}
	}
}
