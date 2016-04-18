using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Adamant.Exploratory.Interpreter
{
	[Serializable]
	public class InterpreterPanicException : Exception
	{
		private List<string> callStack = new List<string>();

		public InterpreterPanicException()
		{
		}

		public InterpreterPanicException(string message)
			: base(message)
		{
		}

		public InterpreterPanicException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected InterpreterPanicException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public void AddCallStack(IEnumerable<string> qualifiedName)
		{
			callStack.Add(string.Join(".", qualifiedName));
		}
	}
}
