using System;

namespace Adamant.Exploratory.Compiler.Core
{
	public interface ISourceText : IComparable<ISourceText>
	{
		string Name { get; }
	}
}
