using System;

namespace Adamant.Exploratory.Common
{
	public struct MatcherWithReturn<TValue, TReturn>
	{
		private readonly TValue value;
		private readonly TReturn returnValue;
		private readonly bool matched;

		internal MatcherWithReturn(TValue value)
		{
			this.value = value;
			returnValue = default(TReturn);
			matched = false;
		}

		internal MatcherWithReturn(TValue value, TReturn returnValue)
		{
			this.value = value;
			this.returnValue = returnValue;
			matched = true;
		}

		public MatcherWithReturn<TValue, TReturn> With<T>(Func<T, TReturn> action)
			where T : class, TValue
		{
			if(matched || value == null) return this;

			var matchedValue = value as T;
			if(matchedValue != null)
				return new MatcherWithReturn<TValue, TReturn>(value, action(matchedValue));

			return this;
		}

		public MatcherWithReturn<TValue, TReturn> Ignore<T>(TReturn result)
			where T : class, TValue
		{
			if(matched || value == null) return this;

			var matchedValue = value as T;
			if(matchedValue != null)
			{
				return new MatcherWithReturn<TValue, TReturn>(value, result);
			}

			return this;
		}

		public MatcherWithReturn<TValue, TReturn> Null(Func<TReturn> action)
		{
			if(matched) return this;
			if(value != null) return this;

			return new MatcherWithReturn<TValue, TReturn>(value, action());
		}

		public TReturn Any(Func<TReturn> action)
		{
			return matched ? returnValue : action();
		}

		public TReturn Exhaustive()
		{
			if(!matched)
				throw new MatchNotExhaustiveException(value);

			return returnValue;
		}
	}

	public struct MatcherWithReturn<TValue, TParam, TReturn>
	{
		private readonly TValue value;
		private readonly TParam param;
		private readonly TReturn returnValue;
		private readonly bool matched;

		internal MatcherWithReturn(TValue value, TParam param)
		{
			this.value = value;
			this.param = param;
			returnValue = default(TReturn);
			matched = false;
		}

		internal MatcherWithReturn(TValue value, TParam param, TReturn returnValue)
		{
			this.value = value;
			this.param = param;
			this.returnValue = returnValue;
			matched = true;
		}

		public MatcherWithReturn<TValue, TParam, TReturn> With<T>(Func<T, TParam, TReturn> action)
			where T : class, TValue
		{
			if(matched || value == null) return this;

			var matchedValue = value as T;
			if(matchedValue != null)
				return new MatcherWithReturn<TValue, TParam, TReturn>(value, param, action(matchedValue, param));

			return this;
		}

		public MatcherWithReturn<TValue, TParam, TReturn> Null(Func<TParam, TReturn> action)
		{
			if(matched) return this;
			if(value != null) return this;

			return new MatcherWithReturn<TValue, TParam, TReturn>(value, param, action(param));
		}

		public TReturn Any(Func<TParam, TReturn> action)
		{
			return matched ? returnValue : action(param);
		}

		public TReturn Exhaustive()
		{
			if(!matched)
				throw new MatchNotExhaustiveException(value);

			return returnValue;
		}
	}
}
