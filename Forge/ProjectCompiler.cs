using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adamant.Exploratory.Compiler;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Forge.Config;
using Newtonsoft.Json;

namespace Adamant.Exploratory.Forge
{
	public class ProjectCompiler
	{
		private readonly string projectFilePath;
		private readonly AdamantCompiler compiler = new AdamantCompiler();
		public event Action<CompiledProject, CompiledProjects> ProjectCompiled;

		public ProjectCompiler(string projectFilePath)
		{
			this.projectFilePath = projectFilePath;
		}

		public CompiledProjects Compile()
		{
			var projects = new CompiledProjects();
			Compile(projectFilePath, projects);
			return projects;
		}

		private void Compile(string projectFilePath, CompiledProjects projects)
		{
			var projectDirPath = Path.GetFullPath(Path.GetDirectoryName(projectFilePath));
			var projectConfig = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText(projectFilePath));
			projectConfig.Name = projectConfig.Name ?? Path.GetFileName(projectDirPath);

			CompileDependencies(projectDirPath, projectConfig, projects);
			CompileProject(projectDirPath, projectConfig, projects);
			CompileSubProjects(projectDirPath, projectConfig, projects);
		}

		private void CompileDependencies(string projectDirPath, ProjectConfig projectConfig, CompiledProjects projects)
		{
			var paths = projectConfig.Dependencies
					.Where(d => !projects.Contains(d.Key))
					.Select(d => DependencyPath(d.Key, d.Value, projectDirPath, projectConfig.DependencyPaths));

			foreach(var path in paths)
				Compile(Path.Combine(path, ProjectFile.Name), projects);
		}

		private static string DependencyPath(string dependencyName, DependencyConfig config, string projectDirPath, IList<string> dependencyPaths)
		{
			if(!string.IsNullOrEmpty(config.Path))
				return Path.Combine(projectDirPath, config.Path);

			foreach(var dependencyPath in dependencyPaths)
			{
				var tryPath = Path.Combine(projectDirPath, dependencyPath, dependencyName);
				if(Directory.Exists(tryPath))
					return tryPath;
			}
			throw new Exception("Could not find dependency");
		}

		private void CompileProject(string projectDirPath, ProjectConfig projectConfig, CompiledProjects projects)
		{
			Console.WriteLine($"Compiling {projectConfig.Name} ...");

			var sourceFiles = new DirectoryInfo(Path.Combine(projectDirPath, "src")).GetFiles("*.adam", SearchOption.AllDirectories);
			var isApp = projectConfig.Template == "app";
			// TODO read trusted from config
			var package = new PackageSyntax(projectConfig.Name, isApp, projectConfig.Dependencies.Select(d => new PackageReferenceSyntax(d.Key, null, true)));
			package = package.With(sourceFiles.Select(fileInfo => compiler.Parse(package, new SourceFile(fileInfo))));
			if(package.Diagnostics.Count > 0)
			{
				PrintDiagnostics(package);
				return;
			}
			var compiledPackage = compiler.Compile(package, projects.Select(p => p.Package));
			var compiledProject = new CompiledProject(projectDirPath, compiledPackage);
			projects.Add(compiledProject);
			OnProjectCompiled(compiledProject, projects);
		}

		protected void OnProjectCompiled(CompiledProject project, CompiledProjects projects)
		{
			ProjectCompiled?.Invoke(project, projects);
		}

		private void CompileSubProjects(string projectDirPath, ProjectConfig projectConfig, CompiledProjects packages)
		{
			// Build Projects that weren't already built as dependencies
			foreach(var project in projectConfig.Projects)
			{
				var projectName = project.Key;
				if(packages.Contains(projectName)) continue;
				Compile(Path.Combine(projectDirPath, project.Value, ProjectFile.Name), packages);
				// TODO copy into target
			}
		}

		private static void PrintDiagnostics(PackageSyntax package)
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
