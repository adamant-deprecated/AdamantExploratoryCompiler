using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class QualifiedName : Name
	{
		public readonly Name Left;
		public readonly SimpleName Right;

		public override TextPosition Position => Left.Position;

		public QualifiedName(Name left, SimpleName right)
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
