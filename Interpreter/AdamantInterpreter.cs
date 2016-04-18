using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Interpreter.References;
using Adamant.Exploratory.Interpreter.Values;

namespace Adamant.Exploratory.Interpreter
{
	public class AdamantInterpreter
	{
		private readonly Package package;

		public AdamantInterpreter(Package package)
		{
			this.package = package;
		}

		public int Invoke(Function entryPoint, params string[] arguments)
		{
			return Invoke(entryPoint, arguments, Console.Out, Console.Error);
		}

		public int Invoke(Function entryPoint, string[] arguments, TextWriter consoleOut, TextWriter consoleError)
		{
			Requires.That(package.EntryPoints.Contains(entryPoint), nameof(entryPoint), "Must be for the package");
			var callStack = new CallStack();
			// TODO pass any arguments
			var returnRef = Call(entryPoint, callStack);

			return 0;
		}

		private Reference Call(Function function, CallStack callStack)
		{
			throw new NotImplementedException();
		}
	}
}
