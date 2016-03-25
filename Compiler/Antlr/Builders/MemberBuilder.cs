using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Members;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class MemberBuilder : Builder<ClassMemberSyntax>
	{
		private readonly IBuildContext build;

		public MemberBuilder(IBuildContext build)
		{
			this.build = build;
		}

		public override ClassMemberSyntax VisitConstructor(AdamantParser.ConstructorContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var name = Identifier(context.identifier());
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new ConstructorSyntax(accessModifier, name, parameters, body);
		}

		public override ClassMemberSyntax VisitDestructor(AdamantParser.DestructorContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new DestructorSyntax(accessModifier, parameters, body);
		}

		public override ClassMemberSyntax VisitField(AdamantParser.FieldContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var isMutableReference = context.kind.Type == AdamantLexer.Var;
			var name = Identifier(context.identifier());
			var type = context.referenceType()?.Accept(build.ReferenceType);
			var initExpression = context.expression()?.Accept(build.Expression);
			return new FieldSyntax(access, isMutableReference, name, type, initExpression);
		}

		public override ClassMemberSyntax VisitAccessor(AdamantParser.AccessorContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var accessorType = context.kind.Type == AdamantParser.Get ? AccessorMethodType.Get : AccessorMethodType.Set;
			var name = Identifier(context.identifier());
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new AccessorMethodSyntax(access, accessorType, name, parameters, body);
		}

		public override ClassMemberSyntax VisitIndexer(AdamantParser.IndexerContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var accessorType = context.kind.Type == AdamantParser.Get ? AccessorMethodType.Get : AccessorMethodType.Set;
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new IndexerMethodSyntax(access, accessorType, parameters, body);
		}

		public override ClassMemberSyntax VisitMethod(AdamantParser.MethodContext context)
		{
			var access = GetAccessModifier(context.modifier());
			var name = Identifier(context.identifier());
			var parameters = build.Parameters(context.parameterList());
			var body = context.methodBody().statement().Select(s => s.Accept(build.Statement));
			return new MethodSyntax(access, name, parameters, body);
		}
	}
}
