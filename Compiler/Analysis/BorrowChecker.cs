using System;
using Adamant.Exploratory.Compiler.Ast;
using Adamant.Exploratory.Compiler.Ast.Declarations;
using Adamant.Exploratory.Compiler.Ast.Expressions;
using Adamant.Exploratory.Compiler.Ast.Members;
using Adamant.Exploratory.Compiler.Ast.Types;
using Adamant.Exploratory.Compiler.Ast.Visitors;

namespace Adamant.Exploratory.Compiler.Analysis
{
	/// <summary>
	/// This class validates the borrow rules and infers ownership
	/// </summary>
	public class BorrowChecker :
		IDeclarationVisitor<Void, Void>,
		IMemberVisitor<Void, Void>,
		IExpressionVisitor<Void, Ownership>
	{
		public void Check(CompilationUnit compilationUnit)
		{
			foreach(var declaration in compilationUnit.Declarations)
				declaration.Accept(this, Void.Value);
		}

		#region Declaration Visitor
		Void IDeclarationVisitor<Void, Void>.VisitClassDeclaration(ClassDeclaration declaration, Void param)
		{
			foreach(var member in declaration.Members)
			{
				member.Accept(this, Void.Value);
			}
			return Void.Value;
		}

		Void IDeclarationVisitor<Void, Void>.VisitFunctionDeclaration(FunctionDeclaration declaration, Void param)
		{
			foreach(var parameter in declaration.Parameters)
			{
				//TODO parameter.Type.DefaultOwnership(Ownership.ImmutableBorrow);
			}

			foreach(var statement in declaration.Body)
			{
				// TODO
			}
			return Void.Value;
		}

		Void IDeclarationVisitor<Void, Void>.VisitGlobalDeclaration(GlobalDeclaration declaration, Void param)
		{
			if(declaration.InitExpression == null)
				declaration.Type.DefaultOwnership(Ownership.Owned);
			else
			{
				var expressionOwnership = declaration.InitExpression.Accept(this, Void.Value);
				declaration.Type.AssignValueWithOwnership(expressionOwnership);
			}
			return Void.Value;
		}
		#endregion

		#region Member Visitor
		Void IMemberVisitor<Void, Void>.VisitConstructor(Constructor member, Void param)
		{
			foreach(var parameter in member.Parameters)
			{
				//TODO parameter.Type.DefaultOwnership(Ownership.ImmutableBorrow);
			}

			throw new NotImplementedException();
		}

		Void IMemberVisitor<Void, Void>.VisitField(Field member, Void param)
		{
			if(member.InitExpression == null)
				member.Type.DefaultOwnership(Ownership.Owned);
			else
			{
				var expressionOwnership = member.InitExpression.Accept(this, Void.Value);
				member.Type.AssignValueWithOwnership(expressionOwnership);
			}
			return Void.Value;
		}

		Void IMemberVisitor<Void, Void>.VisitMethod(Method member, Void param)
		{
			throw new System.NotImplementedException();
		}

		Void IMemberVisitor<Void, Void>.VisitProperty(Property member, Void param)
		{
			throw new System.NotImplementedException();
		}
		#endregion

		#region Expression Visitor
		Ownership IExpressionVisitor<Void, Ownership>.VisitIf(IfExpression ifExpression, Void param)
		{
			throw new NotImplementedException();
		}

		Ownership IExpressionVisitor<Void, Ownership>.VisitBinaryOperator(BinaryOperatorExpression binaryOperatorExpression, Void param)
		{
			throw new NotImplementedException();
		}

		Ownership IExpressionVisitor<Void, Ownership>.VisitCall(CallExpression callExpression, Void param)
		{
			throw new NotImplementedException();
		}

		Ownership IExpressionVisitor<Void, Ownership>.VisitMember(MemberExpression memberExpression, Void param)
		{
			throw new NotImplementedException();
		}

		Ownership IExpressionVisitor<Void, Ownership>.VisitVariable(VariableExpression variableExpression, Void param)
		{
			throw new NotImplementedException();
		}

		Ownership IExpressionVisitor<Void, Ownership>.VisitNew(NewExpression newExpression, Void param)
		{
			throw new NotImplementedException();
		}

		Ownership IExpressionVisitor<Void, Ownership>.VisitNewObject(NewObjectExpression expression, Void param)
		{
			return Ownership.Owned;
		}

		Ownership IExpressionVisitor<Void, Ownership>.VisitLiteral(LiteralExpression expression, Void param)
		{
			return Ownership.ImmutableBorrow;
		}
		#endregion
	}
}
