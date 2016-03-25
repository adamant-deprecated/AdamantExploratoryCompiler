using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.Expressions
{
	public class NewAnonymousObjectSyntax : ExpressionSyntax
	{
		public readonly ValueTypeSyntax BaseClass;
		public readonly IReadOnlyList<ValueTypeSyntax> Interfaces;
		public readonly IReadOnlyList<ExpressionSyntax> Arguments;
		public readonly IReadOnlyList<ClassMemberSyntax> Members;

		public NewAnonymousObjectSyntax(ValueTypeSyntax baseClass, IEnumerable<ValueTypeSyntax> interfaces, IEnumerable<ExpressionSyntax> arguments, IEnumerable<ClassMemberSyntax> members)
		{
			Requires.NotNull(baseClass, nameof(baseClass));

			BaseClass = baseClass;
			Interfaces = interfaces.ToList();
			Arguments = arguments.ToList();
			Members = members.ToList();
		}
	}
}
