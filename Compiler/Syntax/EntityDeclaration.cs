using System;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class EntityDeclaration : SyntaxTree, Declaration, Definition
	{
		protected EntityDeclaration(
			AccessModifier access,
			FullyQualifiedName @namespace,
			Symbol name)
		{
			if(!(access == AccessModifier.Public || access == AccessModifier.Package)) throw new ArgumentOutOfRangeException(nameof(access), "Must be Public or Package");
			if(name == null) throw new ArgumentNullException(nameof(name));

			Namespace = @namespace;
			Access = access;
			Name = name;
			FullyQualifiedName = @namespace.Append(name);
			Definitions = new Definitions(); // TODO actually populate with correct definitions
		}

		public FullyQualifiedName Namespace { get; }
		public AccessModifier Access { get; }
		public Symbol Name { get; }
		public FullyQualifiedName FullyQualifiedName { get; }
		public Definitions Definitions { get; }
	}
}
