using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Common
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T value)
		{
			return enumerable.Concat(value.Yield());
		}
	}
}
