namespace Adamant.Exploratory.Compiler.Semantics
{
	public interface PackageReference
	{
		Package Package { get; }
		string Alias { get; }
		bool Trusted { get; }
	}
}
