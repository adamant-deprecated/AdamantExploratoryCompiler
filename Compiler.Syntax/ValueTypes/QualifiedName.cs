using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Syntax.ValueTypes
{
	public class QualifiedName : Name
	{
		public readonly Name Left;
		public readonly SimpleName Right;

		public QualifiedName(Name left, SimpleName right)
		{
			Requires.NotNull(left, nameof(left));
			Requires.NotNull(right, nameof(right));

			Right = right;
			Left = left;
		}
	}
}
