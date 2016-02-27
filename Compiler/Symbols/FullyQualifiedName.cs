using System;
using System.Diagnostics.Contracts;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class FullyQualifiedName
	{
		private readonly string value;

		public FullyQualifiedName(string name)
		{
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));
			value = Clean(name);
		}

		[Pure]
		public FullyQualifiedName Append(string name)
		{
			return value == null ? new FullyQualifiedName(name) : new FullyQualifiedName(value + "." + name);
		}

		[Pure]
		public FullyQualifiedName Prepend(string name)
		{
			return value == null ? new FullyQualifiedName(name) : new FullyQualifiedName(name + "." + value);
		}

		private static string Clean(string name)
		{
			return name.Replace("@", "");
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
}
