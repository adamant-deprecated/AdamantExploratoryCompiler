using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax.EntityDeclarations
{
	public class FunctionDeclaration : EntityDeclaration
	{
		private readonly IList<Parameter> parameters;
		private readonly IList<Statement> body;

		public FunctionDeclaration(
			AccessModifier access,
			FullyQualifiedName @namespace,
			Symbol name,
			IEnumerable<Parameter> parameters,
			Type returnType,
			IEnumerable<Statement> body)
			: base(access, @namespace, name)
		{
			this.parameters = parameters.ToList();
			ReturnType = returnType;
			this.body = body.ToList();
		}

		public IEnumerable<Parameter> Parameters => parameters;
		public Type ReturnType { get; }
		public IEnumerable<Statement> Body => body;
	}
}
