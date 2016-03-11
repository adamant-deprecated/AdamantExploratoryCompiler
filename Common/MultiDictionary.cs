using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Common
{
	// TODO this doesn't really behave like an IDictionary<TKey, IReadOnlyList<TValue>>
	public class MultiDictionary<TKey, TValue> : IDictionary<TKey, IReadOnlyList<TValue>>
	{
		private static readonly IReadOnlyList<TValue> Empty = new List<TValue>(0);
		private readonly Dictionary<TKey, List<TValue>> items = new Dictionary<TKey, List<TValue>>();

		bool ICollection<KeyValuePair<TKey, IReadOnlyList<TValue>>>.Remove(KeyValuePair<TKey, IReadOnlyList<TValue>> item)
		{
			throw new NotImplementedException();
		}

		public int Count { get; private set; }
		public bool IsReadOnly => false;
		public ICollection<TKey> Keys => items.Keys;
		public IEnumerable<TValue> Values => items.Values.SelectMany(v => v);

		public bool ContainsKey(TKey key)
		{
			return items.ContainsKey(key);
		}

		public void Add(TKey key, TValue value)
		{
			List<TValue> values;
			if(!items.TryGetValue(key, out values))
				items.Add(key, values = new List<TValue>());

			values.Add(value);
			Count += 1;
		}

		public bool Remove(TKey key)
		{
			List<TValue> values;
			if(!items.TryGetValue(key, out values)) return false;

			items.Remove(key);
			Count -= values.Count;
			return true;
		}

		public bool TryGetValue(TKey key, out IReadOnlyList<TValue> value)
		{
			List<TValue> values;
			var found = items.TryGetValue(key, out values);
			value = values;
			return found;
		}

		public IReadOnlyList<TValue> this[TKey key]
		{
			get
			{
				List<TValue> values;
				return items.TryGetValue(key, out values) ? values : Empty;
			}
			set
			{
				List<TValue> values;
				if(items.TryGetValue(key, out values))
					Count -= values.Count;

				items[key] = value.ToList();
				Count += value.Count;
			}
		}

		public IEnumerator<KeyValuePair<TKey, IReadOnlyList<TValue>>> GetEnumerator()
		{
			return items.Select(pair => new KeyValuePair<TKey, IReadOnlyList<TValue>>(pair.Key, pair.Value)).GetEnumerator();
		}

		public void Clear()
		{
			items.Clear();
			Count = 0;
		}

		#region IDictionary implementations
		void IDictionary<TKey, IReadOnlyList<TValue>>.Add(TKey key, IReadOnlyList<TValue> value)
		{
			items.Add(key, value.ToList());
			Count += value.Count;
		}

		ICollection<IReadOnlyList<TValue>> IDictionary<TKey, IReadOnlyList<TValue>>.Values => items.Values.Cast<IReadOnlyList<TValue>>().ToList();
		#endregion

		#region ICollection implementations
		void ICollection<KeyValuePair<TKey, IReadOnlyList<TValue>>>.Add(KeyValuePair<TKey, IReadOnlyList<TValue>> item)
		{
			throw new NotSupportedException();
		}
		bool ICollection<KeyValuePair<TKey, IReadOnlyList<TValue>>>.Contains(KeyValuePair<TKey, IReadOnlyList<TValue>> item)
		{
			throw new NotSupportedException();
		}
		void ICollection<KeyValuePair<TKey, IReadOnlyList<TValue>>>.CopyTo(KeyValuePair<TKey, IReadOnlyList<TValue>>[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}
		#endregion

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}