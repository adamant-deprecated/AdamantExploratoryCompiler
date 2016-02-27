namespace Adamant.Exploratory.Compiler.Syntax.EntityDeclarations
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
