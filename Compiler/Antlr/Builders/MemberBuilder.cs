using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
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
			var access = GetAccessModifier(context.modifier());
			var isMutableReference = context.kind.Type == AdamantLexer.Var;
			var name = Symbol(context.identifier());
			var type = (OwnershipType)context.ownershipType()?.Accept(build.Type);
			var initExpression = context.expression()?.Accept(build.Expression);
			return new Field(access, isMutableReference, name, type, initExpression);
		}

		public override Member VisitProperty(AdamantParser.PropertyContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var isGet = context.kind.Type == AdamantParser.Get;
			var name = Symbol(context.name) ?? new Symbol("[]");
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			var method = new Method(access, isGet ? Property.GetName : Property.SetName, parameters, body);
			return isGet ? new Property(name, method, null) : new Property(name, null, method);
		}

		public override Member VisitMethod(AdamantParser.MethodContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var name = Symbol(context.name);
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new Method(access, name, parameters, body);
		}
	}
}
