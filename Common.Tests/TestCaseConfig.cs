using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Adamant.Exploratory.Common.Tests
{
	public class TestCaseConfig
	{
		public string TestName;
		public string Description;
		public string FileName;
		public bool Runtime;
		public int? Result;
		public bool VerifyConsoleOutput;
		public string ExpectedConsoleOutput;

		public static TestCaseConfig Read(TextReader reader)
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
