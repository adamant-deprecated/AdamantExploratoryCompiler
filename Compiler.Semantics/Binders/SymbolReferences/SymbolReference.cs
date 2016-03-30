﻿using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Syntax;

namespace Adamant.Exploratory.Compiler.Semantics.Binders.SymbolReferences
{
	/// <summary>
	/// A symbol reference referes to a symbol across a compilation
	/// </summary>
	public abstract class SymbolReference
	{
		public abstract string Name { get; }
		public abstract bool IsIn(PackageSyntax package);
		public abstract bool IsVisibleFrom(PackageSyntax package);
		public abstract IEnumerable<SymbolReference> GetMembers(string name);

		public static implicit operator SymbolReference(Entity entity)
		{
			return new EntityReference(entity);
		}
	}
}