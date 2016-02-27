using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Antlr.Builders;
using Adamant.Exploratory.Compiler.Syntax;

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
			var project = new Project(compilationUnits, dependencies);
			// TODO do name binding
			// TODO run borrow checker
			//var borrowChecker = new BorrowChecker();
			//borrowChecker.Check(ast);
			return project;
		}
	}
}
