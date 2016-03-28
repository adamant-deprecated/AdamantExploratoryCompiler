using Adamant.Exploratory.Compiler.Compiled;

namespace Compiler.Emit.Cpp
{
	public class PackageEmitter
	{
		private readonly CompiledPackage package;

		public PackageEmitter(CompiledPackage package)
		{
			this.package = package;
		}
	}
}
