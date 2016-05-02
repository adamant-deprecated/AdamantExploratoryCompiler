using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Adamant.Exploratory.Compiler.Emit.Cpp
{
	public static class CppCompiler
	{
		public static CompilerResult Invoke(string sourcePath, string targetPath)
		{
			var compilerPath = ConfigurationManager.AppSettings["CppCompiler"];
			var libPaths = ConfigurationManager.AppSettings["CppLibPaths"];
			var includePaths = ConfigurationManager.AppSettings["CppIncludePaths"];
			using(var process = new Process())
			{
				process.StartInfo.FileName = compilerPath;
				process.StartInfo.Arguments = $"/EHsc /Fe\"{targetPath}\" {sourcePath}";
				process.StartInfo.WorkingDirectory = Path.GetDirectoryName(sourcePath);
				process.StartInfo.EnvironmentVariables.Add("LIB", libPaths);
				process.StartInfo.EnvironmentVariables.Add("INCLUDE", includePaths);
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				var output = new StringBuilder();
				process.OutputDataReceived += (s, e) => output.AppendLine(e.Data);
				process.ErrorDataReceived += (s, e) => output.AppendLine(e.Data);
				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				return new CompilerResult(process.ExitCode, output.ToString());
			}
		}
	}
}
