using System;
using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Antlr.Builders;
using Adamant.Exploratory.Compiler.Compiled;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Antlr4.Runtime.Atn;

namespace Adamant.Exploratory.Compiler
{
	public class AdamantCompiler
	{
		public CompilationUnit Parse(Package pacakge, SourceFile sourceFile)
		{
			// TODO make use of the package.  We don't currently use the package, but we
			// are taking it as an argument becuase we should be for things like:
			//   * Language Version
			//   * Dependency Names
			//   * Defined Preprocessor Symbols
			var diagnostics = new ParseDiagnosticsBuilder(sourceFile);
			var parser = new AdamantParser(sourceFile.Path);
			// Stupid ANTLR makes it difficult to do this in the constructor
			parser.RemoveErrorListeners();
			var errorsListener = new GatherErrorsListener(diagnostics);
			parser.AddErrorListener(errorsListener);
			parser.Interpreter.PredictionMode = PredictionMode.LlExactAmbigDetection;

			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor(diagnostics);
			tree.Accept(syntaxCheck);

			var compilationUnitBuilder = new CompilationUnitBuilder(sourceFile, diagnostics.Complete());
			return tree.Accept(compilationUnitBuilder);
		}

		public CompiledPackage Compile(Package package, IEnumerable<CompiledPackage> dependencies)
		{
			var diagnostics = new DiagnosticsBuilder(package.Diagnostics);
			var symbolTable = new SymbolTableBuilder(package).Build(diagnostics);

			//var units = compilationUnits.ToList();
			//var globalDefinitions = new DefinitionCollection();
			//foreach(var compilationUnit in units)
			//	AddToDefinitions(globalDefinitions, compilationUnit.Declarations);

			//var projectDependencies = dependencies.ToList();
			//var globalScope = new GlobalScope(globalDefinitions, projectDependencies);
			//foreach(var compilationUnit in units)
			//	compilationUnit.BindNames(globalScope);

			//var project = new Package(packageName, globalDefinitions, projectDependencies);

			// TODO run borrow checker
			//var borrowChecker = new BorrowChecker();
			//borrowChecker.Check(ast);
			//return project;
			throw new NotImplementedException();
		}

		//private void AddToDefinitions(DefinitionCollection globalDefinitions, IEnumerable<Declaration> declarations)
		//{
		//	foreach(var declaration in declarations)
		//	{
		//		declaration.Match()
		//			.With<NamespaceDeclaration>(@namespace => AddNamespaceToDefinitions(globalDefinitions, @namespace))
		//			.With<EntityDeclaration>(entity => globalDefinitions.Add(entity))
		//			.Exhaustive();
		//	}
		//}

		//private void AddNamespaceToDefinitions(DefinitionCollection definitions, NamespaceDeclaration @namespace)
		//{
		//	var containingNamespace = @namespace.Namespace;
		//	foreach(var name in @namespace.Name.Parts())
		//	{
		//		Definition existingDefinition;
		//		NamespaceDefinition namespaceDefinition;
		//		if(definitions.TryGetValue(name, out existingDefinition))
		//		{
		//			namespaceDefinition = existingDefinition as NamespaceDefinition;
		//			if(namespaceDefinition == null)
		//				throw new Exception($"Existing definition conflicts with namespace {containingNamespace.Append(name)}");
		//		}
		//		else
		//			definitions.Add(namespaceDefinition = new NamespaceDefinition(containingNamespace, name));

		//		definitions = namespaceDefinition.Definitions;
		//		containingNamespace = containingNamespace.Append(name);
		//	}

		//	AddToDefinitions(definitions, @namespace.Declarations);
		//}
	}
}
