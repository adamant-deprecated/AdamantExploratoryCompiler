using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Field : NamedMember
	{
		public Field(AccessModifier access, bool isMutableReference, Symbol name, OwnershipType type, Expression initExpression)
			: base(name)
		{
			IsMutableReference = isMutableReference;
			Type = type;
			InitExpression = initExpression;
		}

		public bool IsMutableReference { get; }
		public OwnershipType Type { get; }
		public Expression InitExpression { get; }
		public AccessModifier Access { get; }
	}
}
