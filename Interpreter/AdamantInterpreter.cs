using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Compiler.Semantics.Types.Predefined;
using Adamant.Exploratory.Interpreter.References;
using Adamant.Exploratory.Interpreter.Values;

namespace Adamant.Exploratory.Interpreter
{
	public class AdamantInterpreter
	{
		private readonly Package package;
		private readonly BorrowReference voidReference = new OwnReference(VoidValue.Instance, false, true).Borrow(false);

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
			var exitCode = returnRef.Value.Match().Returning<int>()
				.With<VoidValue>(value => 0)
				.Exhaustive();
			returnRef.Release();
			return exitCode;
		}

		private Reference Call(Function function, CallStack callStack)
		{
			foreach(var statement in function.Body)
			{
				// TODO execute the statement
			}

			// Reached end without return
			if(function.ReturnType.Type is VoidType)
				return voidReference.Borrow();
			else
				throw new InterpreterPanicException("Reached end of function without returning value");

			// TODO catch InterpreterPanicException, add call stack info and rethrow
		}
	}
}
