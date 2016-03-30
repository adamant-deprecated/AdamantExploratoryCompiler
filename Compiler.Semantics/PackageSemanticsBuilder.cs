using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Declarations;
using Adamant.Exploratory.Compiler.Semantics.Binders;
using Adamant.Exploratory.Compiler.Semantics.Model;
using Adamant.Exploratory.Compiler.Syntax;

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
			var package = new PackageModel(packageSyntax);
			var references = GetPackageReferences(package);
			package.Add(references);
			var globalDeclarations = new DeclarationBuilder(packageSyntax).Build();
			BuildDeclarations(package.GlobalNamespace, globalDeclarations);
			package.FindEntryPoints();
			var binders = new BindersBuilder(package).Build(diagnostics);
			// TODO use binders to resolve rest of semantic model
			// TODO type check
			// TODO borrow check
			package.Set(diagnostics);
			return package;
		}

		private IEnumerable<PackageReferenceModel> GetPackageReferences(PackageModel package)
		{
			var packagesLookup = compiledPackages.ToLookup(p => p.Name);
			return packageSyntax.Dependencies.Select(d => new PackageReferenceModel(d, package, packagesLookup[d.Name].Single()));
		}

		private static void BuildDeclarations(NamespaceModel @namespace, IEnumerable<Declaration> declarations)
		{
			foreach(var declaration in declarations)
				declaration.Match()
					.With<NamespaceDeclaration>(ns =>
					{
						var childNamespace = new NamespaceModel(ns.Syntax, @namespace, ns.Name);
						@namespace.Add(childNamespace);
						BuildDeclarations(childNamespace, ns.Members);
					})
					.With<ClassDeclaration>(@classDecl =>
					{
						var syntax = @classDecl.Syntax.Single(); // TODO handle partial classes
						var @class = new ClassModel(syntax, @namespace, syntax.Accessibility, @classDecl.Name);
						@namespace.Add(@class);
					})
					.With<FunctionDeclaration>(@functionDeclaration =>
					{
						var syntax = @functionDeclaration.Syntax.Single(); // TODO handle overloads
						var function = new FunctionModel(syntax, @namespace, syntax.Accessibility, @functionDeclaration.Name);
						@namespace.Add(function);
					})
					// TODO handle ambigouous declarations
					.Exhaustive();
		}
	}
}
