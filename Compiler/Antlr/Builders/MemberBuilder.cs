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
			var name = Identifier(context.identifier());
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
			var name = Identifier(context.identifier());
			var type = (OwnershipType)context.ownershipType()?.Accept(build.Type);
			var initExpression = context.expression()?.Accept(build.Expression);
			return new Field(access, isMutableReference, name, type, initExpression);
		}

		public override Member VisitAccessor(AdamantParser.AccessorContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var accessorType = context.kind.Type == AdamantParser.Get ? AccessorType.Get : AccessorType.Set;
			var name = Identifier(context.name);
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new AccessorMethod(access, accessorType, name, parameters, body);
		}

		public override Member VisitIndexer(AdamantParser.IndexerContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var accessorType = context.kind.Type == AdamantParser.Get ? AccessorType.Get : AccessorType.Set;
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new IndexerMethod(access, accessorType, parameters, body);
		}

		public override Member VisitMethod(AdamantParser.MethodContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var name = Identifier(context.name);
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new Method(access, name, parameters, body);
		}
	}
}
