﻿using Adamant.Exploratory.Common;

namespace Adamant.Exploratory.Compiler.Core.Diagnostics
{
	public class Diagnostic
	{
		public readonly bool IsError;
		public readonly CompilerPhase Phase;
		public readonly ISourceFile File;
		public readonly TextPosition Position;
		public readonly string Message;

		private Diagnostic(bool isError, CompilerPhase phase, ISourceFile file, TextPosition position, string message)
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

		public static Diagnostic ParseError(ISourceFile file, TextPosition position, string message)
		{
			return new Diagnostic(true, CompilerPhase.Parsing, file, position, message);
		}
	}
}
