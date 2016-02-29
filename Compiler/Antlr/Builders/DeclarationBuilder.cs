using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.EntityDeclarations;
using Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations;
using Adamant.Exploratory.Compiler.Syntax.Types;

namespace Adamant.Exploratory.Compiler.Antlr.Builders
{
	public class DeclarationBuilder : Builder<Declaration>, IBuildContext
	{
		private readonly ParameterBuilder parameterBuilder;

		public DeclarationBuilder(FullyQualifiedName currentNamespace)
		{
			CurrentNamespace = currentNamespace;

			parameterBuilder = new ParameterBuilder(this);
			Member = new MemberBuilder(this);
			Expression = new ExpressionBuilder(this);
			Statement = new StatementBuilder(this);
		}

		public FullyQualifiedName CurrentNamespace { get; }
		public TypeBuilder Type { get; } = new TypeBuilder();
		public StatementBuilder Statement { get; }
		public ExpressionBuilder Expression { get; }
		public MemberBuilder Member { get; }

		public IList<Parameter> Parameters(AdamantParser.ParameterListContext context)
		{
			return context._parameters.Select(p => p.Accept(parameterBuilder)).ToList();
		}

		public override Declaration VisitNamespaceDeclaration(AdamantParser.NamespaceDeclarationContext context)
		{
			var name = context.namespaceName()
				._identifiers.Select(Symbol)
				.Aggregate(default(FullyQualifiedName), (ns, n) => ns.Append(n));
			var usingStatements = UsingStatements(context.usingStatement());
			var visitor = new DeclarationBuilder(name);
			var declarations = context.declaration().Select(d => d.Accept(visitor));
			return new NamespaceDeclaration(CurrentNamespace, name, usingStatements, declarations);
		}

		public override Declaration VisitClassDeclaration(AdamantParser.ClassDeclarationContext context)
		{
			// TODO Attributes
			// TODO what about immutable for classes?
			var accessModifier = GetAccessModifier(context.modifier());
			var isPartial = Has(context.modifier(), AdamantLexer.Partial);
			var safety = GetSafety(context.modifier());
			var isAbstract = Has(context.modifier(), AdamantLexer.Abstract);
			var isSealed = Has(context.modifier(), AdamantLexer.Sealed);
			var name = Symbol(context.name);
			// TODO base types
			// TODO type parameter constraints
			var members = context.member().Select(m => m.Accept(Member));
			return new ClassDeclaration(accessModifier, isPartial, safety, isSealed, isAbstract, CurrentNamespace, name, members);
		}

		public override Declaration VisitVariableDeclaration(AdamantParser.VariableDeclarationContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var isMutableReference = context.kind.Type == AdamantLexer.Var;
			var name = Symbol(context.name);
			var type = (OwnershipType)context.ownershipType()?.Accept(Type) ?? OwnershipType.NewInferred();
			var initExpression = context.expression()?.Accept(Expression);
			return new VariableDeclaration(accessModifier, isMutableReference, CurrentNamespace, name, type, initExpression);
		}

		public override Declaration VisitFunctionDeclaration(AdamantParser.FunctionDeclarationContext context)
		{
			var accessModifier = GetAccessModifier(context.modifier());
			var parameters = Parameters(context.parameterList());
			var name = Symbol(context.name);
			var returnType = context.returnType.Accept(Type);
			var body = context.methodBody().statement().Select(s => s.Accept(Statement));
			return new FunctionDeclaration(accessModifier, CurrentNamespace, name, parameters, returnType, body);
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
