using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Syntax.Members
{
	public class Field : Member
	{
		public Field(AccessModifier access, bool isMutableReference, Symbol symbol, OwnershipType type, Expression initExpression)
			: base(access)
		{
			IsMutableReference = isMutableReference;
			Symbol = symbol;
			Type = type;
			InitExpression = initExpression;
		}

		public bool IsMutableReference { get; }
		public Symbol Symbol { get; }
		public OwnershipType Type { get; }
		public Expression InitExpression { get; }
	}
}
