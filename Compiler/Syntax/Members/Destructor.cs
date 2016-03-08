using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Destructor : Member
	{
		private readonly IList<Parameter> parameters;
		private readonly IList<Statement> body;

		public Destructor(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
		{
			Access = access;
			this.parameters = parameters.ToList();
			this.body = body.ToList();
		}

		public AccessModifier Access { get; }
		public IEnumerable<Parameter> Parameters => parameters;
		public IEnumerable<Statement> Body => body;
	}
}
