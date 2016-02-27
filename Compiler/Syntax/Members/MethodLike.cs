using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public abstract class MethodLike : Member
	{
		private readonly IList<Parameter> parameters;
		private readonly IList<Statement> body;

		protected MethodLike(AccessModifier access, IEnumerable<Parameter> parameters, IEnumerable<Statement> body)
			: base(access)
		{
			this.parameters = parameters.ToList();
			this.body = body.ToList();
		}

		public IEnumerable<Parameter> Parameters => parameters;
		public IEnumerable<Statement> Body => body;
	}
}
