using System;
using System.Runtime.Serialization;

namespace Adamant.Exploratory.Common
{
	[Serializable]
	public class MatchNotExhaustiveException : Exception
	{
		public MatchNotExhaustiveException(object value)
			: base($"Match is not exahustive for value '{value}' of type '{value?.GetType().FullName ?? "null"}'")
		{
		}

		protected MatchNotExhaustiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
