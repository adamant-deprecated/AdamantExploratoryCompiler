using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class BlockWithUsingStatements : SyntaxNode
	{
		private readonly List<Declaration> declarations;
		private readonly List<UsingStatement> usingStatements;

		protected BlockWithUsingStatements(IEnumerable<UsingStatement> usingStatements, IEnumerable<Declaration> declarations)
		{

			this.usingStatements = usingStatements.ToList();
			this.declarations = declarations.ToList();
		}

		public IReadOnlyList<UsingStatement> UsingStatements => usingStatements;

		public IReadOnlyList<Declaration> Declarations => declarations;

		public IEnumerable<EntityDeclaration> Entities
		{
			get
			{
				var scopes = new Stack<BlockWithUsingStatements>();
				scopes.Push(this);
				while(scopes.Count != 0)
				{
					var scope = scopes.Pop();
					foreach(var declaration in scope.declarations)
					{
						var entity = declaration.Match().Returning<EntityDeclaration>()
							.With<NamespaceDeclaration>(subScope => { scopes.Push(subScope); return null; })
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
