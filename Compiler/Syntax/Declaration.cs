using System;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Visitors;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class Declaration : Node
	{
		protected Declaration(AccessModifier access, FullyQualifiedName @namespace, Symbol name)
		{
			if(name == null) throw new ArgumentNullException(nameof(name));
			if(!(access == AccessModifier.Public || access == AccessModifier.Package)) throw new ArgumentOutOfRangeException(nameof(access), "Must be Public or Package");

			Access = access;
			Namespace = @namespace;
			Name = name;
			FullyQualifiedName = @namespace.Append(name);
		}

		public AccessModifier Access { get; }
		public FullyQualifiedName Namespace { get; }
		public Symbol Name { get; }
		public FullyQualifiedName FullyQualifiedName { get; }

		public abstract TReturn Accept<TParam, TReturn>(IDeclarationVisitor<TParam, TReturn> visitor, TParam param);
	}
}
