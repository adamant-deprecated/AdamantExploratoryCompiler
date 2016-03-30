using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Model
{
	internal class PackageReferenceModel : SemanticModel<PackageReferenceSyntax>, PackageReference
	{
		public override PackageModel ContainingPackage { get; }
		public new PackageReferenceSyntax Syntax => base.Syntax.Single();
		public Package Package { get; }
		public string Alias => Syntax.Alias;
		public bool Trusted => Syntax.Trusted;

		public PackageReferenceModel(PackageReferenceSyntax syntax, PackageModel containingPackage, Package package)
			: base(syntax)
		{
			Requires.NotNull(containingPackage, nameof(containingPackage));

			ContainingPackage = containingPackage;
			Package = package;
		}
	}
}
