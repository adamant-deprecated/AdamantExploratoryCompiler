namespace Adamant.Exploratory.Common
{
	public static class ObjectExtensions
	{
		public static Matcher<TValue> Match<TValue>(this TValue value)
			where TValue : class
		{
			return new Matcher<TValue>(value);
		}

		public static Matcher<TValue, TParam> Match<TValue, TParam>(this TValue value, TParam param)
			where TValue : class
		{
			return new Matcher<TValue, TParam>(value, param);
		}
	}
}
