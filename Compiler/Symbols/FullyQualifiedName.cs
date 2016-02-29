using System;
using System.Collections.Generic;
using System.Linq;

namespace Adamant.Exploratory.Compiler.Symbols
{
	public class FullyQualifiedName
	{
		private readonly string value;

		public FullyQualifiedName(Symbol name)
		{
			if(name == null) throw new ArgumentNullException(nameof(name));
			value = name.ToString();
		}

		public FullyQualifiedName(FullyQualifiedName @namespace, Symbol name)
		{
			if(@namespace == null) throw new ArgumentNullException(nameof(@namespace));
			if(name == null) throw new ArgumentNullException(nameof(name));
			value = @namespace.value + "." + name;
		}

		public FullyQualifiedName(FullyQualifiedName @namespace, FullyQualifiedName name)
		{
			if(@namespace == null) throw new ArgumentNullException(nameof(@namespace));
			if(name == null) throw new ArgumentNullException(nameof(name));
			value = @namespace.value + "." + name;
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

		public static bool operator ==(FullyQualifiedName left, FullyQualifiedName right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(FullyQualifiedName left, FullyQualifiedName right)
		{
			return !Equals(left, right);
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

		public IEnumerable<FullyQualifiedName> Namespaces()
		{
			for(var name = this; (name = name.Namespace()) != null;)
				yield return name;
		}

		public IEnumerable<Symbol> Parts()
		{
			return value.Split('.').Select(p => new Symbol(p));
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
		public static FullyQualifiedName Append(this FullyQualifiedName @namespace, Symbol name)
		{
			if(name == null) throw new ArgumentNullException(nameof(name));
			return @namespace == null ? new FullyQualifiedName(name) : new FullyQualifiedName(@namespace, name);
		}

		public static FullyQualifiedName Append(this FullyQualifiedName @namespace, FullyQualifiedName name)
		{
			if(name == null) throw new ArgumentNullException(nameof(name));
			return @namespace == null ? name : new FullyQualifiedName(@namespace, name);
		}
	}
}
