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

		public string Emit()
		{
			var source = new SourceFileBuilder();
			source.WriteLine("#pragma once");
			source.WriteLine();

			source.WriteLine("// Dependencies");
			source.WriteLine("#include \"compiler_runtime.cpp\"");
			foreach(var dependency in package.Dependencies)
			{
				source.WriteLine($"#include \"{dependency.Package.Name}.cpp\"");
			}
			source.WriteLine();

			source.WriteLine("namespace");
			source.BeginBlock();
			source.EndBlock();

			return source.ToString();
		}
	}
}
