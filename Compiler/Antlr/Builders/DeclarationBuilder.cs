using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class DeclarationBuilder : Builder<IEnumerable<Declaration>>, IBuildContext
	{
		private readonly ParameterBuilder parameterBuilder;

		public DeclarationBuilder(UsingContext usingContext, QualifiedName currentNamespace)
		{
			UsingContext = usingContext;
			CurrentNamespace = currentNamespace;

			parameterBuilder = new ParameterBuilder(this);
			Member = new MemberBuilder(this);
			Expression = new ExpressionBuilder(this);
			Statement = new StatementBuilder(this);
		}

		public UsingContext UsingContext { get; }
		public QualifiedName CurrentNamespace { get; }
		public TypeBuilder Type { get; } = new TypeBuilder();
		public StatementBuilder Statement { get; }
		public ExpressionBuilder Expression { get; }
		public MemberBuilder Member { get; }

		public IList<Parameter> Parameters(AdamantParser.ParameterListContext context)
		{
			return context._parameters.Select(p => p.Accept(parameterBuilder)).ToList();
		}

		public override IEnumerable<Declaration> VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			var namespaceName = context.namespaceName().GetText();
			var newContext = new UsingContext(UsingContext, GetNamespaces(context.usingStatement()));
			var visitor = new DeclarationBuilder(newContext, CurrentNamespace.Append(namespaceName));
			var declarations = context.declaration().SelectMany(d => d.Accept(visitor));
			return declarations;
		}

		public override IEnumerable<Declaration> VisitClassDeclaration(AdamantParser.ClassDeclarationContext context)
		{
			// TODO Attributes
			// TODO what about immutable for classes?
			var accessModifier = GetAccessModifier(context.modifier());
			var isPartial = Has(context.modifier(), AdamantLexer.Partial);
			var safety = GetSafety(context.modifier());
			var isAbstract = Has(context.modifier(), AdamantLexer.Abstract);
			var isSealed = Has(context.modifier(), AdamantLexer.Sealed);
			var name = context.name.GetText();
			var fullName = CurrentNamespace.Append(name);
			// TODO base types
			// TODO type parameter constraints
			var members = context.member().Select(m => m.Accept(Member));
			yield return new ClassDeclaration(accessModifier, isPartial, safety, isSealed, isAbstract, fullName, members);
		}

		public override IEnumerable<Declaration> VisitGlobalDeclaration(AdamantParser.GlobalDeclarationContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var isMutableReference = context.kind.Type == AdamantLexer.Var;
			var name = context.name.GetText();
			var fullName = CurrentNamespace.Append(name);
			var type = (OwnershipType)context.ownershipType()?.Accept(this) ?? OwnershipType.NewInferred();
			var initExpression = context.expression()?.Accept(Expression);
			yield return new GlobalDeclaration(accessModifier, isMutableReference, fullName, type, initExpression);
		}

		public override IEnumerable<Declaration> VisitFunctionDeclaration(AdamantParser.FunctionDeclarationContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var parameters = Parameters(context.parameterList());
			var name = context.name.GetText();
			var fullName = CurrentNamespace.Append(name);
			var returnType = context.returnType.Accept(Type);
			var body = context.methodBody().statement().Select(s => s.Accept(Statement));
			yield return new FunctionDeclaration(accessModifier, fullName, parameters, returnType, body);
		}

		private static bool Has(AdamantParser.ModifierContext[] modifiers, int desiredModifier)
		{
			return modifiers.Any(modifier => modifier.Symbol.Type == desiredModifier);
		}

		private static Safety GetSafety(AdamantParser.ModifierContext[] modifiers)
		{
			foreach(var modifier in modifiers)
				switch(modifier.Symbol.Type)
				{
					case AdamantLexer.Safe:
						return Safety.ExplicitSafe;
					case AdamantLexer.Unsafe:
						return Safety.Unsafe;
				}

			return Safety.ImplicitSafe;
		}
	}
}
