using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class Declaration : Node, IDeclarationContainer
	{
		protected Declaration(AccessModifier access, QualifiedName name)
		{
			Name = name;
			Access = access;
		}

		public QualifiedName Name { get; private set; }
		public AccessModifier Access { get; private set; }

		IReadOnlyList<Declaration> IDeclarationContainer.Declarations => new[] { this };

		public abstract TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param);
	}
}
