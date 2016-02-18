//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from PreprocessorLineParser.g4 by ANTLR 4.5.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591

namespace Adamant.Exploratory.Compiler.Antlr {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="PreprocessorLineParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.1")]
[System.CLSCompliant(false)]
public interface IPreprocessorLineParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.preprocessorLine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPreprocessorLine([NotNull] PreprocessorLineParser.PreprocessorLineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.preprocessorLine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPreprocessorLine([NotNull] PreprocessorLineParser.PreprocessorLineContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.directive"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDirective([NotNull] PreprocessorLineParser.DirectiveContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.directive"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDirective([NotNull] PreprocessorLineParser.DirectiveContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.define"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDefine([NotNull] PreprocessorLineParser.DefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.define"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDefine([NotNull] PreprocessorLineParser.DefineContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.undefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUndefine([NotNull] PreprocessorLineParser.UndefineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.undefine"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUndefine([NotNull] PreprocessorLineParser.UndefineContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.conditionalSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConditionalSymbol([NotNull] PreprocessorLineParser.ConditionalSymbolContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.conditionalSymbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConditionalSymbol([NotNull] PreprocessorLineParser.ConditionalSymbolContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.ifDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfDecl([NotNull] PreprocessorLineParser.IfDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.ifDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfDecl([NotNull] PreprocessorLineParser.IfDeclContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.elseif"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElseif([NotNull] PreprocessorLineParser.ElseifContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.elseif"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElseif([NotNull] PreprocessorLineParser.ElseifContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.elseDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElseDecl([NotNull] PreprocessorLineParser.ElseDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.elseDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElseDecl([NotNull] PreprocessorLineParser.ElseDeclContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.endif"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEndif([NotNull] PreprocessorLineParser.EndifContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.endif"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEndif([NotNull] PreprocessorLineParser.EndifContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNot([NotNull] PreprocessorLineParser.NotContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Not</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNot([NotNull] PreprocessorLineParser.NotContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Variable</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariable([NotNull] PreprocessorLineParser.VariableContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Variable</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariable([NotNull] PreprocessorLineParser.VariableContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOr([NotNull] PreprocessorLineParser.OrContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Or</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOr([NotNull] PreprocessorLineParser.OrContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnd([NotNull] PreprocessorLineParser.AndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>And</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnd([NotNull] PreprocessorLineParser.AndContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>BooleanValue</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBooleanValue([NotNull] PreprocessorLineParser.BooleanValueContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>BooleanValue</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBooleanValue([NotNull] PreprocessorLineParser.BooleanValueContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Grouping</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGrouping([NotNull] PreprocessorLineParser.GroupingContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Grouping</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGrouping([NotNull] PreprocessorLineParser.GroupingContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEquality([NotNull] PreprocessorLineParser.EqualityContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>Equality</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEquality([NotNull] PreprocessorLineParser.EqualityContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLine([NotNull] PreprocessorLineParser.LineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLine([NotNull] PreprocessorLineParser.LineContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.error"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterError([NotNull] PreprocessorLineParser.ErrorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.error"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitError([NotNull] PreprocessorLineParser.ErrorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="PreprocessorLineParser.warning"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWarning([NotNull] PreprocessorLineParser.WarningContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="PreprocessorLineParser.warning"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWarning([NotNull] PreprocessorLineParser.WarningContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>RegionBegin</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.region"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRegionBegin([NotNull] PreprocessorLineParser.RegionBeginContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>RegionBegin</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.region"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRegionBegin([NotNull] PreprocessorLineParser.RegionBeginContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>RegionEnd</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.region"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRegionEnd([NotNull] PreprocessorLineParser.RegionEndContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>RegionEnd</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.region"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRegionEnd([NotNull] PreprocessorLineParser.RegionEndContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PragmaWarning</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.pragma"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPragmaWarning([NotNull] PreprocessorLineParser.PragmaWarningContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PragmaWarning</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.pragma"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPragmaWarning([NotNull] PreprocessorLineParser.PragmaWarningContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PragmaUnknown</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.pragma"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPragmaUnknown([NotNull] PreprocessorLineParser.PragmaUnknownContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PragmaUnknown</c>
	/// labeled alternative in <see cref="PreprocessorLineParser.pragma"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPragmaUnknown([NotNull] PreprocessorLineParser.PragmaUnknownContext context);
}
} // namespace Adamant.Exploratory.Compiler.Antlr
