using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public interface Package : Symbol<PackageSyntax>
	{
		new PackageSyntax Syntax { get; }
		Namespace GlobalNamespace { get; }
		IEnumerable<PackageReference> Dependencies { get; }
		IReadOnlyList<Diagnostic> Diagnostics { get; }
		IReadOnlyList<Entity<EntitySyntax>> Entities { get; }
		IEnumerable<Function> EntryPoints { get; }
	}
}