using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class Declaration : Node, IDeclarationContainer
	{
		protected Declaration(AccessModifier access, FullyQualifiedName @namespace, Symbol name)
		{
			if(name == null) throw new ArgumentNullException(nameof(name));
			Namespace = @namespace;
			Name = name;
			Access = access;
		}

		public FullyQualifiedName Namespace { get; }
		public Symbol Name { get; }
		public AccessModifier Access { get; }

		IReadOnlyList<Declaration> IDeclarationContainer.Declarations => new[] { this };

		public abstract TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param);
	}
}
