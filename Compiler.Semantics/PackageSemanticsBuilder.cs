using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Declarations;
using Adamant.Exploratory.Compiler.Semantics.Binders;
using Adamant.Exploratory.Compiler.Semantics.Expressions.Literals;
using Adamant.Exploratory.Compiler.Semantics.Statements;
using Adamant.Exploratory.Compiler.Semantics.Types;
using Adamant.Exploratory.Compiler.Semantics.Types.Predefined;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Expressions.Literals;
using Adamant.Exploratory.Compiler.Syntax.Statements;
using Adamant.Exploratory.Compiler.Syntax.ValueTypes;
using ValueType = Adamant.Exploratory.Compiler.Semantics.Types.ValueType;

namespace Adamant.Exploratory.Compiler.Semantics
{
	public class PackageSemanticsBuilder
	{
		private readonly PackageSyntax packageSyntax;
		private readonly IReadOnlyList<Package> compiledPackages;

		public PackageSemanticsBuilder(PackageSyntax packageSyntax, IEnumerable<Package> compiledPackages)
		{
			this.packageSyntax = packageSyntax;
			this.compiledPackages = compiledPackages.ToList();
		}

		public Package Build()
		{
			var diagnostics = new DiagnosticsBuilder(packageSyntax.Diagnostics);
			var package = new Package(packageSyntax);
			var references = GetPackageReferences(package);
			package.Add(references);
			var globalDeclarations = new DeclarationBuilder(packageSyntax).Build();
			BuildDeclarations(package.GlobalNamespace, globalDeclarations);
			package.FindEntities();
			package.FindEntryPoints();
			var binders = new BindersBuilder(package).Build(diagnostics);
			// TODO resolve entity types

			Resolve(package, binders); // use binders to resolve rest of semantic model
									   // TODO type check
									   // TODO borrow check
			package.Set(diagnostics);
			return package;
		}

		private IEnumerable<PackageReference> GetPackageReferences(Package package)
		{
			var packagesLookup = compiledPackages.ToLookup(p => p.Name);
			return packageSyntax.Dependencies.Select(d => new PackageReference(d, package, packagesLookup[d.Name].Single()));
		}

		private static void BuildDeclarations(Namespace @namespace, IEnumerable<Declarations.Declaration> declarations)
		{
			foreach(var declaration in declarations)
				declaration.Match()
					.With<NamespaceDeclaration>(ns =>
					{
						var childNamespace = new Namespace(ns.Syntax, @namespace, ns.Name);
						@namespace.Add(childNamespace);
						BuildDeclarations(childNamespace, ns.Members);
					})
					.With<ClassDeclaration>(@classDecl =>
					{
						var syntax = @classDecl.Syntax.Single(); // TODO handle partial classes
						var @class = new Class(syntax, @namespace, syntax.Accessibility, @classDecl.Name);
						@namespace.Add(@class);
					})
					.With<FunctionDeclaration>(@functionDeclaration =>
					{
						var syntax = @functionDeclaration.Syntax.Single(); // TODO handle overloads
						var function = new Function(syntax, @namespace, syntax.Accessibility, @functionDeclaration.Name);
						@namespace.Add(function);
					})
					// TODO handle ambigouous declarations
					.Exhaustive();
		}

		private static void Resolve(Package package, IReadOnlyDictionary<SyntaxNode, Binder> binders)
		{
			foreach(var entity in package.Entities)
				Resolve(entity, binders);
		}

		private static void Resolve(Entity entity, IReadOnlyDictionary<SyntaxNode, Binder> binders)
		{
			entity.Match()
				.With<Function>(function =>
				{
					function.ReturnType = Resolve(function.ContainingPackage, function.Syntax.ReturnType, binders);
					function.Body = Resolve(function.ContainingPackage, function.Syntax.Body.GetEnumerator(), binders).ToList();
				})
				.Exhaustive();
		}

		private static IEnumerable<Statement> Resolve(Package containingPackage, IEnumerator<StatementSyntax> syntax, IReadOnlyDictionary<SyntaxNode, Binder> binders)
		{
			if(!syntax.MoveNext()) return Enumerable.Empty<Statement>();
			var statementSyntax = syntax.Current;
			var statement = statementSyntax.Match().Returning<Statement>()
				.With<ReturnSyntax>(@return =>
				{
					var expression = @return.Expression != null ? Resolve(containingPackage, @return.Expression, binders) : null;
					return new Return(@return, containingPackage, expression);
				})
				.Exhaustive();

			return statement.Yield().Concat(Resolve(containingPackage, syntax, binders));
		}

		private static Expression Resolve(Package containingPackage, ExpressionSyntax syntax, IReadOnlyDictionary<SyntaxNode, Binder> binders)
		{
			return syntax.Match().Returning<Expression>()
				.With<IntegerLiteralSyntax>(literal => new IntegerLiteral(literal, containingPackage))
				.Exhaustive();
		}

		private static ReferenceType Resolve(Package containingPackage, ReferenceTypeSyntax syntax, IReadOnlyDictionary<SyntaxNode, Binder> binders)
		{
			var valueType = Resolve(containingPackage, syntax.Type, binders);
			return new ReferenceType(syntax, containingPackage, valueType);
		}

		private static ValueType Resolve(Package containingPackage, ValueTypeSyntax syntax, IReadOnlyDictionary<SyntaxNode, Binder> binders)
		{
			return syntax.Match().Returning<ValueType>()
				.With<PredefinedTypeSyntax>(type =>
				{
					switch(type.Name.Text)
					{
						case "void":
							return new VoidType(type, containingPackage);
						case "string":
							return new StringType(type, containingPackage);
						case "byte":
							return new IntType(type, containingPackage, 8, false);
						default:
							var text = type.Name.Text;
							if(text.StartsWith("int"))
								return new IntType(type, containingPackage, ParseBitLength(text.Substring("int".Length)), true);
							if(text.StartsWith("uint"))
								return new IntType(type, containingPackage, ParseBitLength(text.Substring("int".Length)), true);

							throw new NotImplementedException($"Primitive type {text} not implemented");
					}
				})
				.Exhaustive();
		}

		private static int ParseBitLength(string length)
		{
			int bits;
			if(int.TryParse(length, out bits))
				return bits; // TODO should we handle unsual bit lengths?

			return 32;// TODO handle correctly and report error
		}
	}
}
