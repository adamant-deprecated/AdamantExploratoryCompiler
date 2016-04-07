using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public class PackageReference : SourceSemanticNode
	{
		public new PackageReferenceSyntax Syntax => (PackageReferenceSyntax)base.Syntax;
		public Package Package { get; }
		public string Alias => Syntax.Alias;
		public bool Trusted => Syntax.Trusted;

		public PackageReference(PackageReferenceSyntax syntax, Package containingPackage, Package package)
			: base(syntax, containingPackage)
		{
			Requires.NotNull(package, nameof(package));

			Package = package;
		}
	}
}
