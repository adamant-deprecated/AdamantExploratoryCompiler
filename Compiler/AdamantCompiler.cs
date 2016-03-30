using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Antlr.Builders;
using Adamant.Exploratory.Compiler.Binders;
using Adamant.Exploratory.Compiler.Compiled;
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

		public CompiledPackage Compile(PackageSyntax package, IEnumerable<CompiledPackage> dependencies)
		{
			var compiledDependencies = GetCompiledDependencies(package, dependencies);
			var diagnostics = new DiagnosticsBuilder(package.Diagnostics);
			var symbol = new PackageSemanticsBuilder(package).Build(diagnostics);
			var binders = new BindersBuilder(package, symbol, compiledDependencies).Build(diagnostics);

			// TODO type check
			// TODO borrow check
			return new CompiledPackage(package, symbol, FindEntryPoints(symbol), diagnostics.Complete(), compiledDependencies);
		}

		// TODO move to PackageSymbol class
		private static IEnumerable<Function> FindEntryPoints(Container root)
		{
			var containers = new Stack<Container>();
			containers.Push(root);
			while(containers.Count > 0)
			{
				var container = containers.Pop();
				foreach(var symbol in container.GetMembers())
				{
					var entryPoint = symbol.Match().Returning<Function>()
						.With<Namespace>(@namespace =>
						{
							containers.Push(@namespace);
							return null;
						})
						.With<Function>(function => function.Name == "Main" ? function : null)
						.Ignore<Class>(null)
						.Exhaustive();
					if(entryPoint != null)
						yield return entryPoint;
				}
			}
		}

		private static IList<CompiledDependency> GetCompiledDependencies(PackageSyntax package, IEnumerable<CompiledPackage> dependencies)
		{
			var compiledPackages = dependencies.ToLookup(p => p.Name);
			var compiledDependencies = package.Dependencies.Select(d => new CompiledDependency(compiledPackages[d.Name].Single(), d.Alias, d.Trusted));
			return compiledDependencies.ToList();
		}

		public string EmitCpp(CompiledPackage package)
		{
			var emitter = new PackageEmitter(package);
			return emitter.Emit();
		}
	}
}
