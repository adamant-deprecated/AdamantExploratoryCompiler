namespace Adamant.Exploratory.Compiler.Syntax.Declarations
{
	public class Parameter : Node
	{
		public Parameter(string name, Type type)
		{
			Name = name;
			Type = type;
		}

		public string Name { get; }
		public Type Type { get; }
	}
}
