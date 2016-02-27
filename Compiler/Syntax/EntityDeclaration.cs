using System;
using Adamant.Exploratory.Compiler.Symbols;

namespace Adamant.Exploratory.Compiler.Syntax
{
	public abstract class EntityDeclaration : Declaration
	{
		protected EntityDeclaration(
			AccessModifier access,
			FullyQualifiedName @namespace,
			Symbol name)
			: base(@namespace)
		{
			if(!(access == AccessModifier.Public || access == AccessModifier.Package)) throw new ArgumentOutOfRangeException(nameof(access), "Must be Public or Package");
			if(name == null) throw new ArgumentNullException(nameof(name));

			Access = access;
			Name = name;
			FullyQualifiedName = @namespace.Append(name);
		}

		public AccessModifier Access { get; }
		public Symbol Name { get; }
		public FullyQualifiedName FullyQualifiedName { get; }
	}
}
