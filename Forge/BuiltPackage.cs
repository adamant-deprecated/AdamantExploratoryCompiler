using Adamant.Exploratory.Compiler.Compiled;

namespace Adamant.Exploratory.Forge
{
	public class BuiltPackage
	{
		public readonly string ProjectDirectory;
		public readonly CompiledPackage Package;
		public string Name => Package.Name;

		public BuiltPackage(string projectDirectory, CompiledPackage package)
		{
			ProjectDirectory = projectDirectory;
			Package = package;
		}
	}
}
