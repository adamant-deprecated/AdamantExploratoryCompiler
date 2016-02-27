using System.Collections.Generic;
using System.Linq;
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

		public Project CompileProject(IEnumerable<CompilationUnit> compilationUnits, IEnumerable<Project> dependencies)
		{
			var units = compilationUnits.ToList();
			var entities = units.SelectMany(cu => cu.Entities).ToList();
			var projectDependencies = dependencies.ToList();
			var projectGlobals = new GlobalSymbols(entities);
			var globals = projectDependencies.Select(p => p.Globals).ToList();
			foreach(var compilationUnit in units)
				compilationUnit.BindNames(projectGlobals, globals);

			var project = new Project(entities, projectGlobals, projectDependencies);
			// TODO run borrow checker
			//var borrowChecker = new BorrowChecker();
			//borrowChecker.Check(ast);
			return project;
		}
	}
}
