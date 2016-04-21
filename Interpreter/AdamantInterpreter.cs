using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Adamant.Exploratory.Common;
using Adamant.Exploratory.Compiler.Semantics;
using Adamant.Exploratory.Compiler.Semantics.Expressions;
using Adamant.Exploratory.Compiler.Semantics.Expressions.Literals;
using Adamant.Exploratory.Compiler.Semantics.Statements;
using Adamant.Exploratory.Compiler.Semantics.Types.Predefined;
using Adamant.Exploratory.Interpreter.Values;

namespace Adamant.Exploratory.Interpreter
{
	public class AdamantInterpreter
	{
		private readonly Package package;
		private readonly Ref voidReference = Ref.ToStatic(VoidValue.Instance);
		private readonly Dictionary<Literal, Value> literalValues = new Dictionary<Literal, Value>();


		public AdamantInterpreter(Package package)
		{
			this.package = package;
		}

		public int Invoke(Function entryPoint, params string[] arguments)
		{
			return Invoke(entryPoint, arguments, Console.Out, Console.Error);
		}

		public int Invoke(Function entryPoint, string[] arguments, TextWriter consoleOut, TextWriter consoleError)
		{
			Requires.That(package.EntryPoints.Contains(entryPoint), nameof(entryPoint), "Must be for the package");
			// TODO pass any arguments
			using(var result = Call(entryPoint))
			{
				return result.AsExitCode();
			}
		}

		private Ref Call(Function function)
		{
			try
			{
				foreach(var statement in function.Body)
				{
					var returnValue = Execute(statement);
					if(returnValue != null)
						// TODO check that the reference ownership and mutablilty match the return type
						// TODO constrain value to return type
						// TODO constrain integer values to bits of return type
						return returnValue;
				}

				// Reached end without return
				if(function.ReturnType.Type is VoidType)
					return voidReference.Borrow();

				throw new InterpreterPanicException("Reached end of function without returning value");
			}
			catch(InterpreterPanicException ex)
			{
				ex.AddCallStack(function.QualifiedName());
				throw;
			}
		}

		private Ref Execute(Statement statement)
		{
			var result = statement.Match().Returning<Ref>()
				.With<Return>(@return =>
				{
					if(@return.Expression == null) return voidReference.Borrow();
					return Execute(@return.Expression);
				})
				.Exhaustive();
			return result;
		}

		private Ref Execute(Expression expression)
		{
			var result = expression.Match().Returning<Ref>()
				.With<IntegerLiteral>(literal =>
				{
					Value value;
					if(!literalValues.TryGetValue(literal, out value))
					{
						value = new IntegerValue(literal.Value);
						literalValues.Add(literal, value);
					}

					return Ref.ToStatic(value);
				})
				.With<StringLiteral>(literal =>
				{
					Value value;
					if(!literalValues.TryGetValue(literal, out value))
					{
						value = new StringValue(literal.Value);
						literalValues.Add(literal, value);
					}

					return Ref.ToStatic(value);
				})
				.With<MemberAccess>(memberAccess =>
				{
					var reference = Execute(memberAccess.Expression);
					var member = memberAccess.Member;

					return reference.Access(member);
				})
				.Exhaustive();
			return result;
		}
	}
}
