using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Compiler.Analysis;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Ast;

namespace Adamant.Exploratory.Compiler
{
	public class AdamantCompiler
	{
		public Assemblage Parse(string sourcePath)
		{
			var parser = new AdamantParser(sourcePath);
			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor();
			tree.Accept(syntaxCheck);
			var buildAst = new BuildAstVisitor();
			var ast = (Assemblage)tree.Accept(buildAst);
			// TODO run borrow checker
			//var borrowChecker = new BorrowChecker();
			//borrowChecker.Check(ast);
			return ast;
		}

		public Assemblage Combine(IEnumerable<Assemblage> assemblages)
		{
			return new Assemblage(assemblages.SelectMany(a => a.Declarations));
		}
	}
}
