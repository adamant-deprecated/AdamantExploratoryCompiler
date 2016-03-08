using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Method : NamedMember
	{
		private readonly IList<Parameter> parameters;
		private readonly IList<Statement> body;

		public Method(AccessModifier access, Symbol name, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(name)
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
