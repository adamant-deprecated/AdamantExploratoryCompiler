using System;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Core;

namespace Adamant.Exploratory.Compiler
{
	public abstract class SourceText : ISourceText
	{
		public abstract string Name { get; }
		internal abstract AdamantParser NewParser();

		public int CompareTo(ISourceText other)
		{
			return string.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
