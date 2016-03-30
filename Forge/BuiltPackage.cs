using Adamant.Exploratory.Compiler.Semantics;

namespace Adamant.Exploratory.Forge
{
	public class BuiltPackage
	{
		public readonly string ProjectDirectory;
		public readonly Package Package;
		public string Name => Package.Name;

		public BuiltPackage(string projectDirectory, Package package)
		{
			ProjectDirectory = projectDirectory;
			Package = package;
		}
	}
}
