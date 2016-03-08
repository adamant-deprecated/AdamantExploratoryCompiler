using System;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class Symbol
	{
		private readonly string value;

		public Symbol(string symbol)
		{
			if(string.IsNullOrEmpty(symbol)) throw new ArgumentNullException(nameof(symbol));
			if(symbol.Contains("."))
				throw new ArgumentException("Symbol can't contain '.' becuase then it is a Qualified Name");

			value = Clean(symbol);
		}

		private static string Clean(string symbol)
		{
			return symbol.StartsWith("@") ? symbol.Substring(1) : symbol;
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj)) return false;
			if(ReferenceEquals(this, obj)) return true;
			if(obj.GetType() != GetType()) return false;
			return Equals((Symbol)obj);
		}

		protected bool Equals(Symbol other)
		{
			return value == other.value;
		}

		public static bool operator ==(Symbol left, Symbol right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Symbol left, Symbol right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return value;
		}
	}
}
