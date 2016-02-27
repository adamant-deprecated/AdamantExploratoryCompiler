using System;

namespace Adamant.Exploratory.Common
{
	public struct MatcherWithoutReturn<TValue>
		where TValue : class
	{
		private readonly TValue value;
		private readonly bool matched;

		internal MatcherWithoutReturn(TValue value, bool matched)
		{
			this.value = value;
			this.matched = matched;
		}

		public MatcherWithoutReturn<TValue> With<T>(Action<T> action)
			where T : class, TValue
		{
			if(matched || value == null) return this;

			var matchedValue = value as T;
			if(matchedValue != null)
			{
				action(matchedValue);
				return new MatcherWithoutReturn<TValue>(value, true);
			}

			return this;
		}

		public MatcherWithoutReturn<TValue> Null(Action action)
		{
			if(matched) return this;
			if(value != null) return this;

			action();
			return new MatcherWithoutReturn<TValue>(value, true);
		}

		public void Any(Action action)
		{
			if(matched) return;

			action();
		}

		public void Exhaustive()
		{
			if(!matched)
				throw new MatchNotExhaustiveException(value);
		}
	}

	public struct MatcherWithoutReturn<TValue, TParam>
		where TValue : class
	{
		private readonly TValue value;
		private readonly TParam param;
		private readonly bool matched;

		internal MatcherWithoutReturn(TValue value, TParam param, bool matched)
		{
			this.value = value;
			this.param = param;
			this.matched = matched;
		}

		public MatcherWithoutReturn<TValue, TParam> With<T>(Action<T, TParam> action)
			where T : class, TValue
		{
			if(matched || value == null) return this;
			var matchedValue = value as T;
			if(matchedValue == null) return this;

			action(matchedValue, param);
			return new MatcherWithoutReturn<TValue, TParam>(value, param, true);
		}

		public MatcherWithoutReturn<TValue, TParam> Null(Action<TParam> action)
		{
			if(matched) return this;

			if(value == null)
			{
				action(param);
				return new MatcherWithoutReturn<TValue, TParam>(value, param, true);
			}

			return this;
		}

		public void Any(Action<TParam> action)
		{
			if(matched) return;

			action(param);
		}

		public void Exhaustive()
		{
			if(!matched)
				throw new MatchNotExhaustiveException(value);
		}
	}
}
