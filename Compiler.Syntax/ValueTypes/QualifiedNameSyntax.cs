using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class QualifiedNameSyntax : NameSyntax
	{
		public readonly NameSyntax Left;
		public readonly SimpleNameSyntax Right;

		public override TextPosition Position => Left.Position;

		public QualifiedNameSyntax(NameSyntax left, SimpleNameSyntax right)
		{
			Requires.NotNull(left, nameof(left));
			Requires.NotNull(right, nameof(right));

			Right = right;
			Left = left;
		}

		public override string ToString()
		{
			return $"{Left}.{Right}";
		}
	}
}
