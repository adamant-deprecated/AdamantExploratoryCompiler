namespace Adamant.Exploratory.Compiler.Core.Diagnostics
{
	public enum DiagnosticLevel
	{
		Info = 1,
		Warning,
		RuntimeError, // An error which will occur at runtime but doesn't prevent compilation (i.e. pre-condition not met)
		CompilationError, // An error which prevents compilation
	}
}
