using System;
using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Analysis;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Antlr.Builders;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.ScopeDeclarations;

namespace Adamant.Exploratory.Compiler
{
	public class AdamantCompiler
	{
		private static readonly CompilationUnitBuilder CompilationUnitBuilder = new CompilationUnitBuilder();

		public CompilationUnit Parse(string sourcePath)
		{
			var parser = new AdamantParser(sourcePath);
			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor();
			tree.Accept(syntaxCheck);
			return tree.Accept(CompilationUnitBuilder);
		}

		public Package CompileProject(IEnumerable<CompilationUnit> compilationUnits, IEnumerable<Package> dependencies)
		{
			var units = compilationUnits.ToList();
			var globalDefinitions = new Definitions();
			foreach(var compilationUnit in units)
				AddToDefinitions(globalDefinitions, compilationUnit.Declarations);

			var projectDependencies = dependencies.ToList();
			var globalScope = new GlobalScope(globalDefinitions, projectDependencies);
			foreach(var compilationUnit in units)
				compilationUnit.BindNames(globalScope);

			var project = new Package(globalDefinitions, projectDependencies);

			// TODO run borrow checker
			//var borrowChecker = new BorrowChecker();
			//borrowChecker.Check(ast);
			return project;
		}

		private void AddToDefinitions(Definitions globalDefinitions, IEnumerable<Declaration> declarations)
		{
			foreach(var declaration in declarations)
			{
				declaration.Match()
					.With<NamespaceDeclaration>(@namespace => AddNamespaceToDefinitions(globalDefinitions, @namespace))
					.With<EntityDeclaration>(entity => globalDefinitions.Add(entity))
					.Exhaustive();
			}
		}

		private void AddNamespaceToDefinitions(Definitions definitions, NamespaceDeclaration @namespace)
		{
			var containingNamespace = @namespace.Namespace;
			foreach(var name in @namespace.Name.Parts())
			{
				Definition existingDefinition;
				NamespaceDefinition namespaceDefinition;
				if(definitions.TryGetValue(name, out existingDefinition))
				{
					namespaceDefinition = existingDefinition as NamespaceDefinition;
					if(namespaceDefinition == null)
						throw new Exception($"Existing definition conflicts with namespace {containingNamespace.Append(name)}");
				}
				else
					definitions.Add(namespaceDefinition = new NamespaceDefinition(containingNamespace, name));

				definitions = namespaceDefinition.Definitions;
				containingNamespace = containingNamespace.Append(name);
			}

			AddToDefinitions(definitions, @namespace.Declarations);
		}
	}
}
