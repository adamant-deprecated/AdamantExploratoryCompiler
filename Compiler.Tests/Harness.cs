using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Adamant.Exploratory.Compiler.Tests
{
	[TestFixture]
	public class Harness
	{
		private const string Extension = ".adam";

		[Test, TestCaseSource(nameof(TestCases))]
		public void Test(Stream stream)
		{
			Assert.Fail("Test not implemented");
		}

		public IEnumerable<TestCaseData> TestCases()
		{
			var namespaceLength = typeof(Harness).Namespace.Length + 1;
			var assembly = Assembly.GetExecutingAssembly();
			var resourceNames = assembly.GetManifestResourceNames().Where(name => name.EndsWith(Extension));
			foreach(var resourceName in resourceNames)
			{
				var stream = assembly.GetManifestResourceStream(resourceName);
				var testName = resourceName.Substring(namespaceLength, resourceName.Length - namespaceLength - Extension.Length);
				yield return new TestCaseData(stream).SetName(testName);
			}
		}
	}
}
