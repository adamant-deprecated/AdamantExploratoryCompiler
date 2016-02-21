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
			var ast = (CompilationUnit)tree.Accept(buildAst);
			// TODO run borrow checker
			//var borrowChecker = new BorrowChecker();
			//borrowChecker.Check(ast);
			return ast;
		}

		public Project CompileProject(IEnumerable<CompilationUnit> compilationUnits, IEnumerable<Project> dependencies)
		{
			return new Project(compilationUnits);
		}
	}
}
