using Compiler.Emit.Cpp.Properties;

namespace Compiler.Emit.Cpp
{
	public static class CppRuntime
	{
		public static string Source => Resources.Runtime;

		public static string FileName => "compiler_runtime.cpp";
	}
}
