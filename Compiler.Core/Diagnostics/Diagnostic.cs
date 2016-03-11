using System;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Core.Diagnostics
{
	public class Diagnostic : IComparable<Diagnostic>
	{
		public readonly bool IsError;
		public readonly CompilerPhase Phase;
		public readonly ISourceFile File;
		public readonly TextPosition Position;
		public readonly string Message;

		internal Diagnostic(bool isError, CompilerPhase phase, ISourceFile file, TextPosition position, string message)
		{
			Requires.EnumDefined(phase, nameof(phase));
			Requires.NotNull(file, nameof(file));
			Requires.NotNullOrEmpty(message, nameof(message));

			IsError = isError;
			Phase = phase;
			File = file;
			Position = position;
			Message = message;
		}

		public int CompareTo(Diagnostic other)
		{
			var compare = File.CompareTo(other.File);
			if(compare != 0) return compare;
			compare = Position.CompareTo(other.Position);
			if(compare != 0) return compare;
			compare = IsError.CompareTo(other.IsError);
			if(compare != 0) return compare;
			return string.Compare(Message, other.Message, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
