using System;
using System.Runtime.Serialization;

namespace Adamant.Exploratory.Interpreter
{
	[Serializable]
	public class InterpreterPanicException : Exception
	{
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
	}
}
