using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Adamant.Exploratory.Common
{
	public static class Requires
	{
		[Conditional("DEBUG")]
		public static void NotNull<T>(T value, string paramName)
			where T : class
		{
			if(value == null) throw new ArgumentNullException(paramName);
		}

		[Conditional("DEBUG")]
		public static void NotEmpty(string value, string paramName)
		{
			if(value != null && value.Length == 0)
				throw new ArgumentException("Must not be emppty", paramName);
		}

		[Conditional("DEBUG")]
		public static void NotNullOrEmpty(string value, string paramName)
		{
			if(value == null) throw new ArgumentNullException(paramName);
			if(value.Length == 0) throw new ArgumentException("Must not be emppty", paramName);
		}

		[Conditional("DEBUG")]
		public static void EnumDefined(Enum value, string paramName)
		{
			var enumType = value.GetType();
			if(!Enum.IsDefined(enumType, value)) throw new InvalidEnumArgumentException(paramName, (int)(object)value, enumType);
		}

		[Conditional("DEBUG")]
		public static void NonNegative(int value, string paramName)
		{
			if(value < 0) throw new ArgumentOutOfRangeException(paramName, value, "Must be non-negative");
		}

		[Conditional("DEBUG")]
		public static void NonNegative(long value, string paramName)
		{
			if(value < 0) throw new ArgumentOutOfRangeException(paramName, value, "Must be non-negative");
		}

		[Conditional("DEBUG")]
		public static void That(bool condition, string paramName, string message)
		{
			if(!condition) throw new ArgumentException(message, paramName);
		}

		[Conditional("DEBUG")]
		public static void EnumIn<T>(T value, string paramName, params T[] allowedValues)
			where T : struct
		{
			if(!allowedValues.Contains(value)) throw new ArgumentOutOfRangeException(paramName, (int)(object)value, $"Must be one of {string.Join(", ", allowedValues)}");
		}
	}
}
