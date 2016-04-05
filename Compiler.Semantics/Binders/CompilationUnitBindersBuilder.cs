using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Semantics.References;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Declarations;
using Adamant.Exploratory.Compiler.Syntax.Directives;
using Adamant.Exploratory.Compiler.Syntax.Expressions;
using Adamant.Exploratory.Compiler.Syntax.Members;
using Adamant.Exploratory.Compiler.Syntax.Statements;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;
using ValueTypeSyntax = Adamant.Exploratory.Compiler.Syntax.ValueTypeSyntax;

namespace Adamant.Exploratory.Compiler.Semantics.Binders
{
	internal class CompilationUnitBindersBuilder
	{
		private readonly Dictionary<SyntaxNode, Binder> binders;
		private readonly Package package;
		private readonly CompilationUnitSyntax compilationUnit;
		private readonly DiagnosticsBuilder diagnostics;

		public CompilationUnitBindersBuilder(
			Dictionary<SyntaxNode, Binder> binders,
			Package package,
			CompilationUnitSyntax compilationUnit,
			DiagnosticsBuilder diagnostics)
		{
			Requires.NotNull(binders, nameof(binders));
			Requires.NotNull(package, nameof(package));
			Requires.NotNull(compilationUnit, nameof(compilationUnit));
			Requires.NotNull(diagnostics, nameof(diagnostics));

			this.binders = binders;
			this.package = package;
			this.compilationUnit = compilationUnit;
			this.diagnostics = diagnostics;
		}

		public void Build(PackageBinder packageBinder)
		{
			var imports = compilationUnit.UsingDirectives.SelectMany(u => GatherImportedSymbols(u, packageBinder));
			var scope = new CompilationUnitBinder(packageBinder, compilationUnit, imports);

			foreach(var declaration in compilationUnit.Declarations)
				Build(declaration, scope);
		}

		private IEnumerable<ImportedSymbol> GatherImportedSymbols(UsingSyntax usingDirective, Binder scope)
		{
			var lookup = scope.LookupInGlobalNamespace(usingDirective.Name, package);

			if(!lookup.IsViable)
				diagnostics.AddBindingError(compilationUnit.SourceFile, usingDirective.Name.Position, $"Could not bind using statement for {usingDirective.Name}");

			var symbol = lookup.Symbols.Single();
			var @namespace = symbol as NamespaceReference;
			if(@namespace != null)
				return @namespace.GetMembers().Select(m => new ImportedSymbol(m, null));

			return new[] { new ImportedSymbol(symbol, null) };
		}

		public void Build(DeclarationSyntax declaration, ContainerBinder containingScope)
		{
			declaration.Match()
				.With<NamespaceSyntax>(@namespace =>
				{
					var imports = @namespace.UsingDirectives.SelectMany(u => GatherImportedSymbols(u, containingScope));
					var namesCount = @namespace.Names.Count;
					for(var i = 0; i < namesCount; i++)
					{
						var name = @namespace.Names[i];
						var last = i == namesCount - 1;
						var reference = containingScope.GetMembers(name.ValueText).OfType<NamespaceReference>().Single();
						containingScope = new NamespaceBinder(containingScope, reference, last ? imports : Enumerable.Empty<ImportedSymbol>());
						// The innermost binder is the one that has the imports and should be associated with the syntax node
						if(last)
							binders.Add(@namespace, containingScope);
					}

					foreach(var member in @namespace.Members)
						Build(member, containingScope);
				})
				.With<ClassSyntax>(@class =>
				{
					var scope = new ClassBinder(containingScope, @class);
					binders.Add(@class, scope);
					foreach(var member in @class.Members)
						Build(member, scope);
				})
				.With<FunctionSyntax>(function =>
				{
					foreach(var parameter in function.Parameters)
						Build(parameter.Type.Type, containingScope);
					// TODO deal with return type
					Binder scope = new FunctionBinder(containingScope);
					binders.Add(function, scope);
					foreach(var statement in function.Body)
						scope = Build(statement, scope);
				})
				//		.With<VariableDeclaration>(global =>
				//		{
				//			global.Type.BindNames(scope);
				//			global.InitExpression?.BindNames(scope);
				//		})
				.Exhaustive();
		}

		private void Build(ClassMemberSyntax member, ClassBinder containingScope)
		{
			member.Match()
				.With<FieldSyntax>(field =>
				{
					Build(field.Type.Type, containingScope);
					if(field.InitExpression != null)
						Build(field.InitExpression, containingScope);
				})
				.With<ConstructorSyntax>(constructor =>
				{
					foreach(var parameter in constructor.Parameters)
						Build(parameter.Type.Type, containingScope);
					Binder scope = new FunctionBinder(containingScope);
					binders.Add(constructor, scope);
					foreach(var statement in constructor.Body)
						scope = Build(statement, scope);
				})
				.With<DestructorSyntax>(destructor =>
				{
					foreach(var parameter in destructor.Parameters)
						if(parameter.Type != null)
							Build(parameter.Type.Type, containingScope);
					Binder scope = new FunctionBinder(containingScope);
					binders.Add(destructor, scope);
					foreach(var statement in destructor.Body)
						scope = Build(statement, scope);
				})
				.With<IndexerMethodSyntax>(indexer =>
				{
					foreach(var parameter in indexer.Parameters)
						if(parameter.Type != null)
							Build(parameter.Type.Type, containingScope);
					// TODO deal with return type
					Binder scope = new FunctionBinder(containingScope);
					binders.Add(indexer, scope);
					foreach(var statement in indexer.Body)
						scope = Build(statement, scope);
				})
				.With<MethodSyntax>(method =>
				{
					foreach(var parameter in method.Parameters)
						if(parameter.Type != null)
							Build(parameter.Type.Type, containingScope);
					// TODO deal with return type
					Binder scope = new FunctionBinder(containingScope);
					binders.Add(method, scope);
					foreach(var statement in method.Body)
						scope = Build(statement, scope);
				})
				.Exhaustive();
		}

		private void Build(ValueTypeSyntax type, Binder containingScope)
		{
			type.Match()
				.With<PredefinedTypeSyntax>(predefinedType =>
				{
					// Not really sure this makes sense since a predefined type is a keyword
					binders.Add(predefinedType, containingScope);
				})
				.With<GenericNameSyntax>(genericName =>
				{
					binders.Add(genericName, containingScope);
					// TODO associate the type parameters
				})
				.With<IdentifierNameSyntax>(identifierName =>
				{
					binders.Add(identifierName, containingScope);
				})
				.Exhaustive();
		}

		private Binder Build(StatementSyntax statement, Binder containingScope)
		{
			return statement.Match().Returning<Binder>()
				.With<ExpressionStatementSyntax>(expressionStatement =>
				{
					Build(expressionStatement.Expression, containingScope);
					return containingScope;
				})
				.With<ReturnSyntax>(returnStatement =>
				{
					Build(returnStatement.Expression, containingScope);
					return containingScope;
				})
				.Exhaustive();
		}

		private void Build(ExpressionSyntax expression, Binder containingScope)
		{
			expression.Match()
				.With<AssignmentSyntax>(assignment =>
				{
					Build(assignment.LValue, containingScope);
					Build(assignment.RValue, containingScope);
				})
				.With<MemberAccessSyntax>(memberExpression =>
				{
					Build(memberExpression.Expression, containingScope);
				})
				.With<SelfSyntax, IdentifierNameSyntax>(variableExpression =>
				{
					binders.Add(variableExpression, containingScope);
				})
				.With<CallSyntax>(call =>
				{
					Build(call.Expression, containingScope);
					foreach(var argument in call.Arguments)
						Build(argument, containingScope);
				})
				.Ignore<LiteralSyntax>()
				.Exhaustive();
		}
	}
}
