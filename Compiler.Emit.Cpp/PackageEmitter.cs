using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Compiler.Semantics.Expressions;
using Adamant.Exploratory.Compiler.Semantics.Expressions.Literals;
using Adamant.Exploratory.Compiler.Semantics.Statements;
using Adamant.Exploratory.Compiler.Semantics.Types;
using Adamant.Exploratory.Compiler.Semantics.Types.Predefined;
using Adamant.Exploratory.Compiler.Syntax.Expressions;
using ValueType = Adamant.Exploratory.Compiler.Semantics.Types.ValueType;

namespace Compiler.Emit.Cpp
{
	public class PackageEmitter
	{
		private readonly Package package;

		public PackageEmitter(Package package)
		{
			this.package = package;
		}

		public string Emit()
		{
			var source = new SourceFileBuilder();
			source.WriteIndentedLine("#pragma once");
			source.WriteLine();

			source.WriteIndentedLine("// Dependencies");
			source.WriteIndentedLine("#include <cstdint>");
			source.WriteIndentedLine($"#include \"{CppRuntime.FileName}\"");
			foreach(var dependency in package.Dependencies)
				source.WriteIndentedLine($"#include \"{dependency.Package.Name}.cpp\"");

			source.WriteLine();

			source.WriteIndentedLine("namespace");
			source.BeginBlock();
			Emit(source, package.GlobalNamespace.GetMembers());
			source.EndBlock();

			EmitEntryPoint(source);

			return source.ToString();
		}

		private static void Emit(SourceFileBuilder source, IEnumerable<Declaration> declarations)
		{
			foreach(var declaration in declarations)
				declaration.Match()
					.With<Namespace>(ns =>
					{
						source.WriteIndentedLine($"namespace {ns.Name}");
						source.BeginBlock();
						Emit(source, ns.GetMembers());
						source.EndBlock();
					})
					.With<Function>(function =>
					{
						// TODO write correct return type
						// TODO write correct parameter types

						source.WriteIndentedLine($"{TypeOf(function.ReturnType)} {function.Name}()");
						source.BeginBlock();
						Emit(source, function.Body);
						// TODO write body
						source.EndBlock();
					})
					.Exhaustive();
		}

		private static void Emit(SourceFileBuilder source, IReadOnlyList<Statement> statements)
		{
			foreach(var statement in statements)
				Emit(source, statement);
		}

		private static void Emit(SourceFileBuilder source, Statement statement)
		{
			statement.Match()
				.With<Return>(@return =>
				{
					var code = @return.Expression != null ? $"return {CodeFor(@return.Expression)};" : "return;";
					source.WriteIndentedLine(code);
				})
				.Exhaustive();
		}

		private static string CodeFor(Expression expression)
		{
			return expression.Match().Returning<string>()
				.With<IntegerLiteral>(literal =>
				{
					// TODO use the correctly calculated type for this
					return $"new int32_t({literal.Value})";
				})
				.With<StringLiteral>(literal =>
				{
					var encodedValue = Encoding.UTF8.GetBytes(literal.Value);
					var bytes = string.Join(", ", encodedValue.Select(b => "0x" + b.ToString("X")));
					var unsafeArray = $"new uint8_t[{encodedValue.Length}]{{{bytes}}}";
					return $"new ::__Adamant::Runtime::string(new size_t({encodedValue.Length}), {unsafeArray})";
				})
				.With<MemberAccess>(memberAccess => $"({CodeFor(memberAccess.Expression)})->{memberAccess.Member}")
				.With<Cast>(cast =>
				{
					if(cast.CastType != CastType.Panic)
						throw new NotSupportedException($"Cast type '{cast.CastType}' not supported");
					return cast.Type.Match().Returning<string>()
						.With<IntType>(intType => $"new {TypeOf(intType)}(*({CodeFor(cast.Expression)}))")
						.Exhaustive();
				})
				.Exhaustive();
		}

		private static string CodeFor(IEnumerable<string> qualifiedName)
		{
			// Start with :: becuase we are fully qualified and don't want to ever accidently pick up the wrong thing
			return "::" + string.Join("::", qualifiedName);
		}

		private static string TypeOf(ReferenceType type)
		{
			return type.Type.Match().Returning<string>()
				.With<VoidType>(voidType => "void")
				.With<IntType>(intType =>
				{
					var coreType = intType.IsSigned ? "int" : "uint";
					return $"{coreType}{intType.BitLength}_t*";
				})
				.Exhaustive();
		}
		private static string TypeOf(ValueType type)
		{
			return type.Match().Returning<string>()
				.With<VoidType>(voidType => "void")
				.With<IntType>(intType =>
				{
					var coreType = intType.IsSigned ? "int" : "uint";
					return $"{coreType}{intType.BitLength}_t";
				})
				.Exhaustive();
		}

		private void EmitEntryPoint(SourceFileBuilder source)
		{
			var entryPoint = package.EntryPoints.SingleOrDefault();
			if(entryPoint == null) return;

			source.WriteLine();
			source.WriteIndentedLine("int main(int argc, char *argv[])");
			source.BeginBlock();
			var entryPointName = CodeFor(entryPoint.QualifiedName());
			if(entryPoint.ReturnType.Type is VoidType)
			{
				source.WriteIndentedLine($"{entryPointName}();");
				source.WriteIndentedLine("return 0;");
			}
			else
			{
				source.WriteIndentedLine($"auto exitCodePtr = {entryPointName}();");
				source.WriteIndentedLine("auto exitCode = *exitCodePtr;");
				source.WriteIndentedLine("delete exitCodePtr;");
				source.WriteIndentedLine("return exitCode;");
			}
			source.EndBlock();
		}
	}
}
