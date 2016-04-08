﻿using System.Collections.Generic;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Compiler.Semantics.Expressions.Literals;
using Adamant.Exploratory.Compiler.Semantics.Statements;
using Adamant.Exploratory.Compiler.Semantics.Types;
using Adamant.Exploratory.Compiler.Semantics.Types.Predefined;
using Adamant.Exploratory.Compiler.Syntax;

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
					var code = @return.Expression != null ? $"return {CodeFor(source, @return.Expression)};" : "return;";
					source.WriteIndentedLine(code);
				})
				.Exhaustive();
		}

		private static string CodeFor(SourceFileBuilder source, Expression expression)
		{
			return expression.Match().Returning<string>()
				.With<IntegerLiteral>(literal =>
				{
					// TODO use the correctly calculated type for this
					return $"new int32_t({literal.Value})";
				})
				.Exhaustive();
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

		private void EmitEntryPoint(SourceFileBuilder source)
		{
			var entryPoint = package.EntryPoints.SingleOrDefault();
			if(entryPoint == null) return;

			source.WriteLine();
			source.WriteIndentedLine("int main(int argc, char *argv[])");
			source.BeginBlock();
			source.WriteIndentedLine($"{entryPoint.Name}();");
			source.WriteIndentedLine("return 0;");
			source.EndBlock();
		}
	}
}
