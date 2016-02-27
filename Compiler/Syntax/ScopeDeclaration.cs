using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class ScopeDeclaration : Declaration
	{
		private readonly List<Declaration> declarations;

		protected ScopeDeclaration(FullyQualifiedName @namespace, IEnumerable<Declaration> declarations)
			: base(@namespace)
		{
			this.declarations = declarations.ToList();
		}

		public IReadOnlyList<Declaration> Declarations => declarations;

		public IEnumerable<EntityDeclaration> Entities
		{
			get
			{
				var scopes = new Stack<ScopeDeclaration>();
				scopes.Push(this);
				while(scopes.Count != 0)
				{
					var scope = scopes.Pop();
					foreach(var declaration in scope.declarations)
					{
						var entity = declaration.Match().Returning<EntityDeclaration>()
							.With<ScopeDeclaration>(subScope => { scopes.Push(subScope); return null; })
							.With<EntityDeclaration>(e => e)
							.Exhaustive();
						if(entity != null)
							yield return entity;
					}
				}
			}
		}
	}
}
