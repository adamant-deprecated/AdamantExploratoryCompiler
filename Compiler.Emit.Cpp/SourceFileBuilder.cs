using System;
using System.Text;

namespace Compiler.Emit.Cpp
{
	public class SourceFileBuilder
	{
		private readonly StringBuilder code = new StringBuilder();
		private readonly StringBuilder indent = new StringBuilder();
		private bool firstElement;

		public void Write(string value)
		{
			code.Append(value);
			firstElement = false;
		}

		public void WriteIndented(string value)
		{
			code.Append(indent);
			code.Append(value);
			firstElement = false;
		}

		public void WriteIndent()
		{
			code.Append(indent);
		}

		public void WriteLine(string value)
		{
			code.AppendLine(value);
			firstElement = false;
		}

		public void WriteIndentedLine(string value)
		{
			code.Append(indent);
			code.AppendLine(value);
			firstElement = false;
		}

		public void WriteLine()
		{
			code.AppendLine();
			firstElement = false;
		}

		public void BlankLine()
		{
			if(firstElement)
				firstElement = false;
			else
				code.AppendLine();
		}

		public void BeginBlock()
		{
			code.AppendLine(indent + "{");
			indent.Append('\t');
			firstElement = true;
		}

		public void EndBlock()
		{
			if(indent.Length == 0)
				throw new InvalidOperationException("Can't outdent top level");

			indent.Length = indent.Length - 1;
			code.AppendLine(indent + "}");
			firstElement = false;
		}
	}
}
