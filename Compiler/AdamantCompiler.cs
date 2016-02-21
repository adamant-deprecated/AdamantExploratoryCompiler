using System.Collections.Generic;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Ast;

namespace Adamant.Exploratory.Compiler
{
	public class AdamantCompiler
	{
		public CompilationUnit Parse(string sourcePath)
		{
			var parser = new AdamantParser(sourcePath);
			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor();
			tree.Accept(syntaxCheck);
			var buildAst = new BuildAstVisitor();
			return (CompilationUnit)tree.Accept(buildAst);
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
