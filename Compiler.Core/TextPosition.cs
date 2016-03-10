using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Core
{
	/// <summary>
	/// Represents a position in a source file. All values are zero based.  Newline chars should be
	/// reported as on the line they terminate.
	/// </summary>
	public struct TextPosition
	{
		public readonly long Offset;
		public readonly int Line;
		public readonly int Column;

		public TextPosition(long offset, int line, int column)
		{
			Requires.NonNegative(offset, nameof(offset));
			Requires.NonNegative(line, nameof(line));
			Requires.NonNegative(column, nameof(column));

			Offset = offset;
			Line = line;
			Column = column;
		}
	}
}
