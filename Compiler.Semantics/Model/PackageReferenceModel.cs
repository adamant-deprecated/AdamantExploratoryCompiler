using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	internal class PackageReferenceModel : SemanticElementModel<PackageReferenceSyntax>, PackageReference
	{
		public Package Package { get; }
		public string Alias => Syntax.Alias;
		public bool Trusted => Syntax.Trusted;

		public PackageReferenceModel(PackageReferenceSyntax syntax, PackageModel containingPackage, Package package)
			: base(syntax, containingPackage)
		{
			Requires.NotNull(package, nameof(package));

			Package = package;
		}
	}
}
