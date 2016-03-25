using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class Location
	{
		public readonly ISourceText File;
		public readonly TextPosition Position;

		public Location(ISourceText file, TextPosition position)
		{
			Requires.NotNull(file, nameof(file));

			File = file;
			Position = position;
		}
	}
}
