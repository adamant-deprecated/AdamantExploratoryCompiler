using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Adamant.Exploratory.Compiler.Core;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Compiler.Syntax;
using Compiler.Emit.Cpp;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Adamant.Exploratory.Compiler.Tests
{
	[TestFixture]
	public class Harness
	{
		private const string Extension = ".adam";
		private readonly AdamantCompiler compiler = new AdamantCompiler();
		private readonly PackageReferenceSyntax runtimeDependency = new PackageReferenceSyntax("System.Runtime", null, true);

		private string WorkPath;

		[TestFixtureSetUp]
		public void SetUp()
		{
			WorkPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			Directory.CreateDirectory(WorkPath);
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			Directory.Delete(WorkPath, true);
		}

		[Test, TestCaseSource(nameof(TestCases))]
		public void Test(TestCaseConfig config, TextReader reader)
		{
			var dependencies = config.Runtime ? new[] { runtimeDependency } : Enumerable.Empty<PackageReferenceSyntax>();
			var package = new PackageSyntax($"Adamant.Exploratory.Compiler.Tests.{config.TestName}", dependencies);
			var unit = compiler.Parse(package, new SourceReader(config.FileName, reader));
			package = package.With(new[] { unit });
			if(package.Diagnostics.Count > 0)
				Assert.Fail(ToString(package.Diagnostics));
			var compiledPackage = compiler.Compile(package, Enumerable.Empty<Package>());
			if(compiledPackage.Diagnostics.Count > 0)
				Assert.Fail(ToString(compiledPackage.Diagnostics));

			var cppSource = compiler.EmitCpp(compiledPackage);
			var cppSourceName = compiledPackage.Name + ".cpp";
			CreateFile(cppSourceName, cppSource);
			CreateFile(CppRuntime.FileName, CppRuntime.Source);
			var targetPath = Path.Combine(WorkPath, compiledPackage.Name + ".exe");
			var result = CppCompiler.Invoke(Path.Combine(WorkPath, cppSourceName), targetPath);
			if(result.ExitCode != 0)
			{
				result.WriteOutputToConsole();
				Assert.Fail("C++ compiler error");
			}

			// Execute the app
			using(var process = new Process())
			{
				process.StartInfo.FileName = targetPath;
				process.StartInfo.WorkingDirectory = Path.GetDirectoryName(WorkPath);
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				var outputBuffer = new StringBuilder();
				var errorBuffer = new StringBuilder();
				process.OutputDataReceived += (s, e) => outputBuffer.AppendLine(e.Data);
				process.ErrorDataReceived += (s, e) => errorBuffer.AppendLine(e.Data);
				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();

				if(config.Result != null)
					Assert.AreEqual(config.Result, process.ExitCode, "Exit Code");
				if(config.VerifyConsoleOutput)
					Assert.AreEqual(config.ExpectedConsoleOutput, outputBuffer.ToString());
			}
		}

		private void CreateFile(string fileName, string content)
		{
			using(var file = File.CreateText(Path.Combine(WorkPath, fileName)))
			{
				file.Write(content);
			}
		}

		private static string ToString(IReadOnlyList<Diagnostic> diagnostics)
		{
			var builder = new StringBuilder();
			ISourceText file = null;
			foreach(var diagnostic in diagnostics)
			{
				if(file != diagnostic.File)
				{
					file = diagnostic.File;
					builder.AppendLine($"In {file.Name}");
				}
				var level = diagnostic.Level.ToString();
				var line = diagnostic.Position.Line + 1;
				var column = diagnostic.Position.Column + 1;
				builder.AppendLine($"{level} on line {line} at character {column}: ");
				builder.AppendLine($"    {diagnostic.Message}");
			}
			return builder.ToString();
		}

		public IEnumerable<TestCaseData> TestCases()
		{
			var namespaceLength = typeof(Harness).Namespace.Length + 1;
			var assembly = Assembly.GetExecutingAssembly();
			var resourceNames = assembly.GetManifestResourceNames().Where(name => name.EndsWith(Extension));
			foreach(var resourceName in resourceNames)
			{
				var stream = assembly.GetManifestResourceStream(resourceName);
				var reader = new StreamReader(stream);
				var config = ReadConfig(reader);
				config.FileName = resourceName;
				config.TestName = resourceName.Substring(namespaceLength, resourceName.Length - namespaceLength - Extension.Length);
				var testCaseData = new TestCaseData(config, reader).SetName(config.TestName);
				if(!string.IsNullOrWhiteSpace(config.Description))
					testCaseData.SetDescription(config.Description);
				yield return testCaseData;
			}
		}

		public static TestCaseConfig ReadConfig(TextReader reader)
		{
			var line = reader.ReadLine().Trim();
			Assert.AreEqual("/*---", line);
			var lines = new List<string>();
			string rawLine;
			while((line = (rawLine = reader.ReadLine()).Trim()) != "---" && line != "*/")
				lines.Add(rawLine);

			var vson = string.Join("\r\n", lines);
			var config = JsonConvert.DeserializeObject<TestCaseConfig>(vson);

			if(line == "---")
			{
				// read in expected console output
				lines.Clear();
				while((line = (rawLine = reader.ReadLine()).Trim()) != "*/")
					lines.Add(rawLine);

				config.VerifyConsoleOutput = true;
				config.ExpectedConsoleOutput = string.Join("\r\n", lines);
			}

			return config;
		}
	}
}
