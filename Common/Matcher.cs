using System;

namespace Adamant.Exploratory.Common
{
	public static class Matcher
	{
		public static Matcher<TValue> For<TValue>(TValue value)
			where TValue : class
		{
			return new Matcher<TValue>(value);
		}

		public static Matcher<TValue, TParam> For<TValue, TParam>(TValue value, TParam param)
			where TValue : class
		{
			return new Matcher<TValue, TParam>(value, param);
		}

		public static MatcherWithoutReturn<TValue> WithoutReturn<TValue>(TValue value)
			where TValue : class
		{
			return new MatcherWithoutReturn<TValue>(value, false);
		}

		// TODO create the rest of these
	}

	public struct Matcher<TValue>
			where TValue : class
	{
		private readonly TValue value;

		internal Matcher(TValue value)
		{
			this.value = value;
		}

		public MatcherWithoutReturn<TValue> With<T>(Action<T> @case)
			where T : class, TValue
		{
			return new MatcherWithoutReturn<TValue>(value, false).With(@case);
		}

		public MatcherWithReturn<TValue, TReturn> Returning<TReturn>()
		{
			return new MatcherWithReturn<TValue, TReturn>(value);
		}
	}

	public struct Matcher<TValue, TParam>
		where TValue : class
	{
		private readonly TValue value;
		private readonly TParam param;

		internal Matcher(TValue value, TParam param)
		{
			this.value = value;
			this.param = param;
		}

		public MatcherWithoutReturn<TValue, TParam> With<T>(Action<T, TParam> @case)
			where T : class, TValue
		{
			return new MatcherWithoutReturn<TValue, TParam>(value, param, false).With(@case);
		}

		public MatcherWithReturn<TValue, TParam, TReturn> Returning<TReturn>()
		{
			return new MatcherWithReturn<TValue, TParam, TReturn>(value, param);
		}
	}
}
