using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Antlr.Builders;
using Adamant.Exploratory.Compiler.Compiled;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Symbols;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Directives;
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
			var builder = new ParseDiagnosticsBuilder(sourceFile);
			var parser = new AdamantParser(sourceFile.Path);
			// Stupid ANTLR makes it difficult to do this in the constructor
			parser.RemoveErrorListeners();
			var errorsListener = new GatherErrorsListener(builder);
			parser.AddErrorListener(errorsListener);
			parser.Interpreter.PredictionMode = PredictionMode.LlExactAmbigDetection;

			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor(builder);
			tree.Accept(syntaxCheck);

			var diagnostics = builder.Complete();
			if(diagnostics.Any())
				return new CompilationUnit(sourceFile, Enumerable.Empty<UsingDirective>(), Enumerable.Empty<Declaration>(), diagnostics);

			var compilationUnitBuilder = new CompilationUnitBuilder(sourceFile, diagnostics);
			return tree.Accept(compilationUnitBuilder);
		}

		public CompiledPackage Compile(Package package, IEnumerable<CompiledPackage> dependencies)
		{
			var diagnostics = new DiagnosticsBuilder(package.Diagnostics);
			// TODO incorperate dependencies into package symbol
			var symbolTable = new SymbolTableBuilder(package).Build(diagnostics);
			// TODO bind names to symbols
			// TODO type check
			// TODO borrow check
			return new CompiledPackage(package, symbolTable.Package, symbolTable.Symbols);
		}
	}
}
