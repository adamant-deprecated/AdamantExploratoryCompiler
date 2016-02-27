using System;
using System.Diagnostics.Contracts;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class FullyQualifiedName
	{
		private readonly string value;

		public FullyQualifiedName(Symbol symbol)
		{
			if(symbol == null) throw new ArgumentNullException(nameof(symbol));
			value = symbol.ToString();
		}

		public FullyQualifiedName(FullyQualifiedName name, Symbol symbol)
		{
			if(name == null) throw new ArgumentNullException(nameof(name));
			if(symbol == null) throw new ArgumentNullException(nameof(symbol));
			value = name.value + "." + symbol;
		}

		private FullyQualifiedName(string fullyQualifiedName)
		{
			value = fullyQualifiedName;
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as FullyQualifiedName;
			return other != null && Equals(value, other.value);
		}

		public override string ToString()
		{
			return value;
		}

		public FullyQualifiedName Namespace()
		{
			if(!value.Contains("."))
				return null;

			return new FullyQualifiedName(value.Substring(0, value.LastIndexOf('.')));
		}

		public string Name()
		{
			if(!value.Contains("."))
				return value;

			return value.Substring(value.LastIndexOf('.') + 1);
		}
	}

	public static class FullyQualifiedNameExtensions
	{
		public static FullyQualifiedName Append(this FullyQualifiedName name, Symbol symbol)
		{
			if(symbol == null) throw new ArgumentNullException(nameof(symbol));
			return name == null ? new FullyQualifiedName(symbol) : new FullyQualifiedName(name, symbol);
		}
	}
}
