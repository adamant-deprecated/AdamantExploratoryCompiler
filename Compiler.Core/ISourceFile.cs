using System;

namespace Adamant.Exploratory.Compiler.Core
{
	public interface ISourceFile: IComparable<ISourceFile>
	{
		string Name { get; }
	}
}
