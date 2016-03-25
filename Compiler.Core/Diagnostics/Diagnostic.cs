using System;
using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Core.Diagnostics
{
	public class Diagnostic : IComparable<Diagnostic>
	{
		public readonly DiagnosticLevel Level;
		public readonly CompilerPhase Phase;
		public readonly ISourceText File;
		public readonly TextPosition Position;
		public readonly string Message;

		internal Diagnostic(DiagnosticLevel level, CompilerPhase phase, ISourceText file, TextPosition position, string message)
		{
			Requires.EnumDefined(level, nameof(level));
			Requires.EnumDefined(phase, nameof(phase));
			Requires.NotNull(file, nameof(file));
			Requires.NotNullOrEmpty(message, nameof(message));

			Level = level;
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
			compare = Level.CompareTo(other.Level);
			if(compare != 0) return compare;
			return string.Compare(Message, other.Message, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
