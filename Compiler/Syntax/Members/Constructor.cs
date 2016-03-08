using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Constructor : Member
	{
		private readonly IList<Parameter> parameters;
		private readonly IList<Statement> body;

		public Constructor(AccessModifier access, Symbol name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
		{
			Access = access;
			Name = name;
			this.parameters = parameters.ToList();
			this.body = body.ToList();
		}

		public AccessModifier Access { get; }
		public Symbol Name { get; }
		public IEnumerable<Parameter> Parameters => parameters;
		public IEnumerable<Statement> Body => body;
	}
}
