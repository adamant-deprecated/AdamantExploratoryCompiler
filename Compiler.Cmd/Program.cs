using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Adamant.Exploratory.Compiler.Antlr;
using Adamant.Exploratory.Compiler.Cmd.Options;
using Antlr4.Runtime;
using NDesk.Options;
using NDesk.Options.Extensions;

namespace Adamant.Exploratory.Compiler.Cmd
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var options = ParseOptions(args);
			if(options == null) return;

			switch(options.Action)
			{
				case CmdAction.Lex:
					ProcessSources(options.SourcePaths, options.OutputPath, Lex);
					break;
				case CmdAction.PrintTree:
					ProcessSources(options.SourcePaths, options.OutputPath, PrintTree);
					break;
				case CmdAction.Validate:
					throw new NotImplementedException();
				default:
					throw new InvalidEnumArgumentException();
			}
		}

		private static CmdOptions ParseOptions(string[] args)
		{
			var options = new OptionSet();
			var lex = options.AddSwitch("l|lex", "Run the lexer and output the tokens");
			var printTree = options.AddSwitch("t|tree", "Print the parse tree");
			var validate = options.AddSwitch("v|validate", "Run compilation and produce errors, but don't emit anything");
			var outputPath = options.AddVariable<string>("o|output", "Path to write output to");

			var filePatterns = options.Parse(args);
			var workingDirectory = Directory.GetCurrentDirectory();
			var sourcePaths = filePatterns.SelectMany(pattern => Directory.GetFiles(workingDirectory, pattern)).ToArray();

			#region Single Action
			if(lex && printTree)
			{
				Console.WriteLine("Can't lex and print tree at same time");
				return null;
			}

			if(lex && validate)
			{
				Console.WriteLine("Can't lex and validate at same time");
				return null;
			}

			if(printTree && validate)
			{
				Console.WriteLine("Can't print tree and validate at same time");
				return null;
			}
			#endregion

			if(sourcePaths.Length == 0)
				Console.WriteLine("Must specify at least one source file");

			if(sourcePaths.Any(sourcePath => !HasAdamantExtension(sourcePath)))
				Console.WriteLine("All source files must have Adamant Extension (*.adam)");

			if(lex)
			{
				return new CmdOptions()
				{
					Action = CmdAction.Lex,
					SourcePaths = sourcePaths,
					OutputPath = outputPath.Value,
				};
			}

			if(printTree)
			{
				return new CmdOptions()
				{
					Action = CmdAction.PrintTree,
					SourcePaths = sourcePaths,
					OutputPath = outputPath.Value,
				};
			}

			if(validate)
			{
				return new CmdOptions()
				{
					Action = CmdAction.Validate,
					SourcePaths = sourcePaths,
					OutputPath = outputPath.Value,
				};
			}

			Console.WriteLine("Must specify an action.  One of -l -t -v");
			return null;
		}

		private static bool HasAdamantExtension(string filePath)
		{
			return filePath != null && Path.GetExtension(filePath) == ".adam";
		}

		private static void ProcessSources(string[] sourcePaths, string outputPath, Action<string, TextWriter> action)
		{
			var output = outputPath != null ? File.CreateText(outputPath) : Console.Out;

			foreach(var sourcePath in sourcePaths)
			{
				output.WriteLine();
				output.WriteLine($"Processing: {sourcePath}");
				action(sourcePath, output);
			}
		}

		private static void Lex(string codePath, TextWriter output)
		{
			var lexer = new AdamantLexer(codePath);
			var tokens = new CommonTokenStream(lexer);
			tokens.Fill();
			foreach(var token in tokens.GetTokens())
				output.WriteLine(Format(token));
		}

		private static string Format(IToken token)
		{
			var channel = token.Channel > 0 ? ",channel=" + AdamantLexer.ChannelNames[token.Channel] : "";
			var text = token.Text != null
				? "'" + token.Text.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\n", "\\t") + "'"
				: "<no text>";
			var type = AdamantLexer.DefaultVocabulary.GetSymbolicName(token.Type);
			return text + ":" + type + channel + " @" + token.Line + ":" + token.Column;
		}

		private static void PrintTree(string codePath, TextWriter output)
		{
			var parser = new AdamantParser(codePath) { BuildParseTree = true };
			var tree = parser.compilationUnit();
			var syntaxCheck = new SyntaxCheckVisitor();
			tree.Accept(syntaxCheck);
			output.WriteLine(tree.ToStringTree(parser));
		}
	}
}
