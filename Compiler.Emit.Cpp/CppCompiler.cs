using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace Compiler.Emit.Cpp
{
	public static class CppCompiler
	{
		public static int Invoke(string sourcePath, string targetPath)
		{
			var compilerPath = ConfigurationManager.AppSettings["CppCompiler"];
			var libPath = ConfigurationManager.AppSettings["CppLibPath"];
			using(var process = new Process())
			{
				process.StartInfo.FileName = compilerPath;
				process.StartInfo.Arguments = $"/EHsc /Fe\"{targetPath}\" {sourcePath}";
				process.StartInfo.WorkingDirectory = Path.GetDirectoryName(sourcePath);
				process.StartInfo.EnvironmentVariables.Add("LIB", libPath);
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.OutputDataReceived += ProcessOutputHandler;
				process.ErrorDataReceived += ProcessOutputHandler;
				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				return process.ExitCode;
			}
		}

		private static void ProcessOutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
		{
			//* Do your stuff with the output (write to console/log/StringBuilder)
			Console.WriteLine(outLine.Data);
		}
	}
}
