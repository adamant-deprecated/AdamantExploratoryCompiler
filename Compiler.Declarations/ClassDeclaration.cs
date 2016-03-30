using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.Declarations;

namespace Adamant.Exploratory.Compiler.Declarations
{
	public class ClassDeclaration : Declaration
	{
		public readonly IReadOnlyList<ClassSyntax> Syntax;

		public ClassDeclaration(string name, IEnumerable<ClassSyntax> syntax)
			: base(name)
		{
			Syntax = syntax.ToList();
		}

		public ClassDeclaration(ClassSyntax syntax)
			: base(syntax.Name.ValueText)
		{
			Syntax = new List<ClassSyntax>(1) { syntax };
		}
	}
}
