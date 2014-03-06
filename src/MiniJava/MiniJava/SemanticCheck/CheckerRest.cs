using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniJava.Frontend.Trees;
using MiniJava.Frontend;

namespace MiniJava.SemanticCheck
{
	public partial class Checker
	{
		// The method visitors return the type of the expression node, or null if some error occured.
		// Obviously the visitors must check if the subexpression types evaluate to null!
		public JavaType visitExpr(BinopTree op)
		{
			string opName = String.Format("the binary '{0}' operator", op.Operator.ToOperator());

			switch (op.Operator)
			{
				case Binop.Assign:
					{
						JavaType rhsTyp = evalExpr(op.Rhs);
						if (!op.Lhs.IsLValueExpr)	
						{
							Context.AddError(new CompileError("Lvalue expression expected in assignment", op.Lhs.Location));
							return null;
						}
						JavaType lhsTyp = evalExpr(op.Lhs);
						if (rhsTyp == null || lhsTyp == null)
							return null;
						if(!isAssignableTo(rhsTyp, lhsTyp))
						{
							Context.AddError(new CompileError(
								String.Format("Value of type '{0}' is not assignable to lvalue of type '{1}'", rhsTyp, lhsTyp),
								op.Location));
							return null;
						}
						return lhsTyp;
					}
				case Binop.And:
				case Binop.Or:
					return checkExprsHaveTypes(op.Lhs, op.Rhs, JavaType.Bool, opName, op.Location);
				case Binop.Over:
				case Binop.Minus:
				case Binop.Plus:
				case Binop.Times:
				case Binop.Mod:
					return checkExprsHaveTypes(op.Lhs, op.Rhs, JavaType.Int, opName, op.Location);
				case Binop.EQ:
				case Binop.NE:
					{
						JavaType rhsTyp = evalExpr(op.Rhs);
						JavaType lhsTyp = evalExpr(op.Lhs);
						if (rhsTyp == null || lhsTyp == null)
							return JavaType.Bool;
						if(!isAssignableTo(lhsTyp, rhsTyp) && !isAssignableTo(rhsTyp, lhsTyp))
						{
							Context.AddError(new CompileError(
								String.Format("Operands to operator '{0}' require operands with similar types, got '{1}' and '{2}'", opName, lhsTyp, rhsTyp),
								op.Location));
							return null;
						}
						return JavaType.Bool;
					}
				case Binop.LT:
				case Binop.LE:
				case Binop.GT:
				case Binop.GE:
					checkExprsHaveTypes(op.Lhs, op.Rhs, JavaType.Int, opName, op.Location);
					return JavaType.Bool;
				default:
					throw new InvalidProgramException();
			}
		}

		public JavaType visitExpr(LiteralValueTree tree)
		{
			if (tree.Value is long)
			{
				long val = (long)tree.Value;
				if (val < (long)Int32.MinValue || val > (long)Int32.MaxValue)
					Context.AddError(new CompileError("Overflow in integer literal", tree.Location));
			}
			return tree.Type;
		}

		public JavaType visitExpr(UnopTree tree)
		{
			switch (tree.Operator)
			{
				case Unop.Minus:
					return checkExprHasType(tree.Operand, JavaType.Int, "Operand to the unary '-' operator");
				case Unop.Not:
					return checkExprHasType(tree.Operand, JavaType.Bool, "Operand to the unary '!' operator");
				default:
					throw new InvalidProgramException();
			}
		}
		public JavaType visitExpr(VariableTree tree)
		{
			return bindVariable(tree);
		}
		public JavaType visitExpr(ThisLiteralTree tree)
		{
			if (MethodContext.Method.IsMain)
			{
				Context.Errors.Add(new CompileError("Cannot reference 'this' in static context", tree.Location));
				return null;
			}
			else
				return new JavaClassType(MethodContext.Class.Name);
		}

		public JavaType visitExpr(ArrayLengthExprTree alet)
		{
			checkExprHasArrayType(alet.Expr);
			return JavaType.Int;
		}

		public JavaType visitExpr(ArrayAccessExprTree arrayAccessExprTree)
		{
			JavaType baseType = checkExprHasArrayType(arrayAccessExprTree.ArrayExpr);
			checkExprHasType(arrayAccessExprTree.IndexExpr, JavaType.Int, "Array indexing expression");
			return baseType;
		}

		public JavaType visitExpr(NullLiteralTree nullLiteralTree)
		{
			return JavaType.NullConstantType;
		}
		public JavaType visitExpr(FieldAccessExprTree tree)
		{
			JavaType objTyp = evalExpr(tree.Expr);
			if (objTyp == null) return null;
			if (checkTypeValidity(objTyp, "field access operator", tree.Location,
				allowPrimitive: false, allowArray: false))
			{
				ClassTree cls = lookupClass(objTyp);
				Tuple<FieldTree, ClassTree, bool> result = resolveField(cls, tree.Field, tree.Location, true);
				if (result == null)
				{
					Context.AddError(new CompileError(
						String.Format("No field '{0}' in class '{1}'", tree.Field, MethodContext.Class.Name),
						tree.Location));
					return null;
				}
				if (!result.Item3)
					return null;
				tree.Binding = new FieldBinding(result.Item2, result.Item1);
				return result.Item1.Type;
			}
			return null;
		}

		public JavaType visitExpr(MethodCallExprTree tree)
		{
			// obj.method(arg1, arg2): Four things to check: eval arguments, object, and method. Then compare expected method types.

			// First: Arguments
			List<JavaType> argTypes = tree.Args.Select(arg => evalExpr(arg)).ToList();

			// next: object
			var objectTyp = evalExpr(tree.Expr);
			if(objectTyp == null || !checkTypeValidity(objectTyp, "object to be called", tree.Expr.Location,
				allowPrimitive: false, allowArray: false))
				return null;
			ClassTree cls = lookupClass(objectTyp);

			// Then: method
			Tuple<MethodTree, ClassTree> result = lookupMethod(cls, tree.Method);
			if (result == null)
			{
				Context.AddError(new CompileError(
					String.Format("No method '{0}' found in class '{1}'", tree.Method, cls.Name),
					tree.Location));
				return null;
			}
			MethodTree meth = result.Item1;
			tree.ResolvedMethod = meth;
			// Then: argument types

			if (argTypes.Count != meth.Arguments.Count)
			{
				Context.AddError(new CompileError(
					String.Format("Wrong number of arguments when calling method '{0}', expected {1}, but got {2}",
						meth.Name, meth.Arguments.Count, argTypes.Count),
					tree.Location));
				return null;
			}
			for(int i = 0; i < argTypes.Count; i++)
			{
				if(argTypes[i] != null && !isAssignableTo(argTypes[i], meth.Arguments[i].Type))
				{
					Context.AddError(new CompileError(
						String.Format("Type '{0}' not convertible to type of argument {1} of method '{2}': '{3}'",
						argTypes[i], i + 1, meth.Name, meth.Arguments[i].Type),
						tree.Args.ElementAt(i).Location));
				}
			}
			return meth.ReturnType;
		}

		public JavaType visitExpr(NewExprTree tree)
		{
			if (tree.ArrayLengthExpr != null)
				checkExprHasType(tree.ArrayLengthExpr, JavaType.Int, "Array length expression");

			if (!checkTypeValidity(tree.ObjectType, "new expression", tree.Location,
				allowPrimitive: tree.ArrayLengthExpr != null, allowArray: false))
				return null;
			return tree.ArrayLengthExpr == null ? tree.ObjectType : new JavaArrayType(tree.ObjectType);
		}

		/*
		 * The statement checkers perform very simple control flow analysis to avoid "return statement expected" errors.
		 * The algorithm is quite simple: for each invidual statement, evaluate whether it is "definitely returning",
		 * i.e it is possible to control flow to go past this statement. This is the boolean value returned by the following methods.
		 * A method is invalid, if the body (always a StmtList) is not definitely returning.
		 * 
		 * The rules are as follows:
		 * return <expr>: always d.r.
		 * if (E) S1 else S2: d.r if and only if S1 and S2 are d.r
		 * { S1; S2; .. Sn; }: d.r iff any of Sm are d.r; also, complain about unreachable code if there are statements after Sm.
		 * 
		 * if (E) S1: never d.r
		 * Others: never d.r
		 */
		public bool visitStmt(AssertStmtTree tree)
		{
			checkExprHasType(tree.Expr, JavaType.Bool, "Value in assert expression");
			return false;
		}

		public bool visitStmt(VariableDeclTree tree)
		{
			declareVariable(tree, false);
			return false;
		}

		public bool visitStmt(WhileStmtTree tree)
		{
			checkExprHasType(tree.Condition, JavaType.Bool, "Condition in 'while' statement");
			tree.Body.Accept(this);
			return false;
		}

		public bool visitStmt(PrintStmtTree tree)
		{
			if (tree.Expr == null)
				return false;

			JavaType type = evalExpr(tree.Expr);
			if (type != null && type.Equals(JavaType.Void))
				Context.AddError(new CompileError("Non-void expression expected", tree.Expr.Location));

			return false;
		}

		public bool visitStmt(IfStmtTree tree)
		{
			checkExprHasType(tree.Condition, JavaType.Bool, "Condition in 'if' statement");
			bool thenDR = tree.Then.Accept(this);
			bool elseDR = tree.Else != null ? tree.Else.Accept(this) : false;
			return thenDR && elseDR;
		}

		public bool visitStmt(ReturnStmtTree tree)
		{
			if (tree.Expr == null)
			{
				if (!MethodContext.Method.ReturnType.Equals(JavaType.Void))
					Context.AddError(new CompileError(
						String.Format("Expression returning type '{0}' expected", MethodContext.Method.ReturnType),
						tree.Location));
			}
			else
			{
				JavaType retvalType = evalExpr(tree.Expr);
				if (retvalType == JavaType.Void)
					Context.AddError(new CompileError("Non-void expression expected", tree.Expr.Location));
				else if (retvalType != null && !isAssignableTo(retvalType, MethodContext.Method.ReturnType))
				{
					Context.AddError(new CompileError(
						String.Format("Type of returned value '{0}' not assignable to type '{1}'", retvalType, MethodContext.Method.ReturnType),
						tree.Expr.Location));
				}
			}
			return true;
		}

		public bool visitStmt(ExprStmtTree exprStmtTree)
		{
			evalExpr(exprStmtTree.Expr);
			return false;
		}

		public bool visitStmtList(StmtListTree tree)
		{
			return MethodContext.WithNewScope(() =>
			{
				StmtTree firstUnreachable = null;
				bool foundDR = false;
				foreach (var stmt in tree.Statements)
				{
					if (foundDR && firstUnreachable == null)
						firstUnreachable = stmt;
					if (stmt.Accept(this))
						foundDR = true;
				}
				if (firstUnreachable != null)
					Context.AddError(new CompileError("Unreachable code detected", firstUnreachable.Location));
				return foundDR;
			});
		}

	}
}
