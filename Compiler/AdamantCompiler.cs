using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Antlr.Builders;
using Adamant.Exploratory.Compiler.Core.Diagnostics;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Compiler.Syntax;
using Adamant.Exploratory.Compiler.Syntax.Directives;
using Antlr4.Runtime.Atn;
using Compiler.Emit.Cpp;

namespace Adamant.Exploratory.Compiler
{
	public class AdamantCompiler
	{
		public CompilationUnitSyntax Parse(PackageSyntax package, SourceText sourceText)
		{
			// TODO make use of the package.  We don't currently use the package, but we
			// are taking it as an argument becuase we should be for things like:
			//   * Language Version
			//   * Dependency Names
			//   * Defined Preprocessor Symbols
			var builder = new ParseDiagnosticsBuilder(sourceText);
			var parser = sourceText.NewParser();
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
				return new CompilationUnitSyntax(sourceText, Enumerable.Empty<UsingSyntax>(), Enumerable.Empty<DeclarationSyntax>(), diagnostics);

			var compilationUnitBuilder = new CompilationUnitBuilder(sourceText, diagnostics);
			return tree.Accept(compilationUnitBuilder);
		}

		public Package Compile(PackageSyntax package, IEnumerable<Package> compiledPackages)
		{
			return new PackageSemanticsBuilder(package, compiledPackages).Build();
		}

		public string EmitCpp(Package package)
		{
			var emitter = new PackageEmitter(package);
			return emitter.Emit();
		}
	}
}
