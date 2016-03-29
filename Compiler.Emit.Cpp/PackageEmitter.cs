using System.Linq;
using Adamant.Exploratory.Compiler.Compiled;
using Adamant.Exploratory.Compiler.Symbols;

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
			source.WriteIndentedLine("#pragma once");
			source.WriteLine();

			source.WriteIndentedLine("// Dependencies");
			source.WriteIndentedLine($"#include \"{CppRuntime.FileName}\"");
			foreach(var dependency in package.Dependencies)
				source.WriteIndentedLine($"#include \"{dependency.Package.Name}.cpp\"");

			source.WriteLine();

			source.WriteIndentedLine("namespace");
			source.BeginBlock();
			Emit(source, package.Symbol);
			source.EndBlock();

			EmitEntryPoint(source);

			return source.ToString();
		}

		private void Emit(SourceFileBuilder source, ContainerSymbol container)
		{
			//throw new System.NotImplementedException();
		}

		private void EmitEntryPoint(SourceFileBuilder source)
		{
			var entryPoint = package.EntryPoints.SingleOrDefault();
			if(entryPoint == null) return;

			source.WriteLine();
			source.WriteIndentedLine("int main(int argc, char *argv[])");
			source.BeginBlock();
			source.WriteIndentedLine("return 0;");
			source.EndBlock();
		}
	}
}
