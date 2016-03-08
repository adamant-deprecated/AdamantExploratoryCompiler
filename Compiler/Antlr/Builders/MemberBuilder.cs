using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Members;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class MemberBuilder : Builder<Member>
	{
		private readonly IBuildContext build;

		public MemberBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override Member VisitConstructor(AdamantParser.ConstructorContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var name = Symbol(context.identifier());
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new Constructor(accessModifier, name, parameters, body);
		}

		public override Member VisitDestructor(AdamantParser.DestructorContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new Destructor(accessModifier, parameters, body);
		}

		public override Member VisitField(AdamantParser.FieldContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var isMutableReference = context.kind.Type == AdamantLexer.Var;
			var name = Symbol(context.identifier());
			var type = (OwnershipType)context.ownershipType()?.Accept(build.Type);
			var initExpression = context.expression()?.Accept(build.Expression);
			return new Field(accessModifier, isMutableReference, name, type, initExpression);
		}

		public override Member VisitProperty(AdamantParser.PropertyContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new Property(accessModifier, parameters, body);
		}

		public override Member VisitMethod(AdamantParser.MethodContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new Method(accessModifier, parameters, body);
		}
	}
}
